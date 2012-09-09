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
    using System.Web.Http;

    public class CatalogsController : ApiController
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly ICatalogRepository _catalogRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandBus _commandBus;

        public CatalogsController(ICatalogRepository catalogRepository, IUnitOfWork unitOfWork, ICommandBus commandBus)
        {
            _catalogRepository = catalogRepository;
            _unitOfWork = unitOfWork;
            _commandBus = commandBus;
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

                    _catalogRepository.Add(new Catalog() { Name = Text.Movies, User_Id = user.Id });
                    _catalogRepository.Add(new Catalog() { Name = Text.Albums, User_Id = user.Id });
                    _catalogRepository.Add(new Catalog() { Name = Text.Books, User_Id = user.Id });

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

            CreateOrUpdateCatalogCommand cmd = new CreateOrUpdateCatalogCommand(catalog.Id, catalog.Name, user.Id);
            _commandBus.Submit(cmd);

            return catalog;
        }
    }
}
