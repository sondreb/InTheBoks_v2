using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InTheBoks.Data;
using InTheBoks.Models;
using InTheBoks.Resources;
using InTheBoks.Security;

namespace InTheBoks.Web.Api
{
    public class CatalogsController : ApiController
    {
        private readonly DataContext _context;

        public CatalogsController()
        {
            _context = new DataContext();
        }

        [Authorize]
        public IQueryable<Catalog> Get()
        {
            var user = (FacebookIdentity)User.Identity;

            var any = _context.Catalogs.Where(c => c.User_Id == user.Id).Any();

            if (!any) // Populate some default catalogs for new users.
            {
                _context.Catalogs.Add(new Catalog() { Name = Text.Movies, User_Id = user.Id });
                _context.Catalogs.Add(new Catalog() { Name = Text.Albums, User_Id = user.Id });
                _context.Catalogs.Add(new Catalog() { Name = Text.Books, User_Id = user.Id });
                _context.SaveChanges();
            }

            var query = _context.Catalogs.Where(c => c.User_Id == user.Id).OrderBy(c => c.Name);

            return query;
        }
    }
}
