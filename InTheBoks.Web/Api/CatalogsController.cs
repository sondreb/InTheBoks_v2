namespace InTheBoks.Web.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using InTheBoks.Data;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Models;
    using InTheBoks.Resources;
    using InTheBoks.Security;
    using NLog;

    public class CatalogsController : ApiController
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
        private ICatalogRepository _catalogRepository;
        private IUnitOfWork _unitOfWork;

        public CatalogsController(ICatalogRepository catalogRepository, IUnitOfWork unitOfWork)
        {
            _catalogRepository = catalogRepository;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
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
    }
}
