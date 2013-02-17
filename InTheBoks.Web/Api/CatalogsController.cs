namespace InTheBoks.Web.Api
{
    using InTheBoks.Commands;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Dispatcher;
    using InTheBoks.Models;
    using InTheBoks.Resources;
    using InTheBoks.Security;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    [Authorize]
    public class CatalogsController : BaseApiController
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly ICatalogRepository _catalogRepository;
        private readonly ICommandBus _commandBus;
        private readonly IUnitOfWork _unitOfWork;

        public CatalogsController(ICatalogRepository catalogRepository, IUnitOfWork unitOfWork, ICommandBus commandBus)
        {
            _catalogRepository = catalogRepository;
            _unitOfWork = unitOfWork;
            _commandBus = commandBus;
        }

        public void Delete(long id)
        {
            var user = (FacebookIdentity)User.Identity;

            var dbItem = _catalogRepository.GetById(id);

            if (dbItem == null)
            {
                throw new ItemNotFoundException("Catalog does not exists.");
            }

            if (dbItem.User_Id != user.Id)
            {
                _log.Warn("Someone is potentially trying to delete another users catalog. User ID: " + user.Id);
                throw new ItemNotFoundException("Catalog does not exists.");
            }

            // Submit a delete operation to any handlers.
            _commandBus.Submit(new DeleteCatalogCommand(dbItem.Id));

            //return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public IEnumerable<Catalog> Get()
        {
            try
            {
                var user = (FacebookIdentity)User.Identity;

                var any = _catalogRepository.Query().Where(c => c.User_Id == user.Id).Any();

                if (!any) // Populate some default catalogs for new users.
                {
                    _log.Debug("Creating Sample Catalogs for User ID: " + user.Id);

                    _catalogRepository.Add(new Catalog() { Name = Text.Movies, User_Id = user.Id, Created = DateTime.UtcNow, Modified = DateTime.UtcNow });
                    _catalogRepository.Add(new Catalog() { Name = Text.Albums, User_Id = user.Id, Created = DateTime.UtcNow, Modified = DateTime.UtcNow });
                    _catalogRepository.Add(new Catalog() { Name = Text.Books, User_Id = user.Id, Created = DateTime.UtcNow, Modified = DateTime.UtcNow });

                    _unitOfWork.Commit();
                }

                var query = _catalogRepository.Query().Where(c => c.User_Id == user.Id).OrderBy(c => c.Name);

                return query.ToList();
            }
            catch (Exception ex)
            {
                _log.ErrorException("Unable to retreive catalogs", ex);
                throw ex;
            }
        }

        public Catalog Post(Catalog catalog)
        {
            var user = (FacebookIdentity)User.Identity;

            if (catalog.Id > 0) // TODO: Investigate why catalogs are -1 and not 0.
            {
                throw new InvalidOperationException("Post should be used for new catalogs. Use the Put method for updates.");
            }

            CreateOrUpdateCatalogCommand cmd = new CreateOrUpdateCatalogCommand(catalog.Id, catalog.Name, user.Id, catalog.Visibility);
            var result = _commandBus.Submit(cmd);

            if (result.Success)
            {
                catalog.Id = result.Id;
                return catalog;
            }
            else
            {
                throw new ApplicationException("Unable to save catalog");
            }
        }

        // UPDATE
        public Catalog Put(int id, Catalog item)
        {
            if (item.Id == 0)
            {
                throw new InvalidOperationException("Put should be used for updating items. Use the Post method for creations.");
            }

            var dbItem = _catalogRepository.Query().Where(i => i.Id == item.Id && i.User_Id == User.Id);

            if (dbItem == null)
            {
                // This probably means someone is trying to update someone elses item. Let's verify so we can log
                // all attempts to gain illicit access.

                var existsOnAnotherUser = _catalogRepository.Query().Where(i => i.Id == item.Id).Any();

                if (existsOnAnotherUser)
                {
                    _log.Fatal("Someone is trying to update another user's catalog. User ID: " + User.Id + " Catalog ID: " + item.Id);
                }
                else
                {
                    _log.Error("User is trying to access item that does not exists. User ID: " + User.Id + " Catalog ID: " + item.Id);
                }

                throw new ItemNotFoundException("Catalog does not exists.");
            }
            else
            {
                CreateOrUpdateCatalogCommand cmd = new CreateOrUpdateCatalogCommand(item.Id, item.Name, User.Id, item.Visibility);
                var result = _commandBus.Submit(cmd);

                if (result.Success)
                {
                    //item.Id = result.Id;
                    return item;
                }
                else
                {
                    throw new Exception("Failed to save catalog.");
                }
            }
        }
    }
}