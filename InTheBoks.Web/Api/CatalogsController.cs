using System;
namespace InTheBoks.Web.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using InTheBoks.Data;
    using InTheBoks.Models;
    using InTheBoks.Resources;
    using InTheBoks.Security;
    using NLog;

    public class CatalogsController : ApiController
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly DataContext _context;

        public CatalogsController()
        {
            _log.Debug("Creating CatalogsController");

            try
            {
                _context = new DataContext();
            }
            catch (Exception ex)
            {
                _log.ErrorException("Unable to create CatalogsController", ex);
            }
        }

        [Authorize]
        public IEnumerable<Catalog> Get()
        {
            //ObjectContent<IEnumerable<Product>> responseContent = new ObjectContent<IEnumerable<Product>>(db.Products.Include(p => p.ProductSubcategory).AsEnumerable(), new XmlMediaTypeFormatter()); // change the formatters accordingly
            //MemoryStream ms = new MemoryStream();

            //// This line would cause the formatter's WriteToStream method to be invoked.
            //// Any exceptions during WriteToStream would be thrown as part of this call
            //responseContent.CopyToAsync(ms).Wait();

            try
            {
                _log.Debug("Get Catalogs");

                var user = (FacebookIdentity)User.Identity;

                var any = _context.Catalogs.Where(c => c.User_Id == user.Id).Any();

                if (!any) // Populate some default catalogs for new users.
                {
                    _log.Debug("Creating Sample Catalogs for User ID: " + user.Id);

                    _context.Catalogs.Add(new Catalog() { Name = Text.Movies, User_Id = user.Id });
                    _context.Catalogs.Add(new Catalog() { Name = Text.Albums, User_Id = user.Id });
                    _context.Catalogs.Add(new Catalog() { Name = Text.Books, User_Id = user.Id });
                    _context.SaveChanges();
                }

                var query = _context.Catalogs.Where(c => c.User_Id == user.Id).OrderBy(c => c.Name);

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
