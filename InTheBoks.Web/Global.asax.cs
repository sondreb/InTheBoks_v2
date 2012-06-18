using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Facebook;
using InTheBoks.Data;
using InTheBoks.Security;
using NLog;

namespace InTheBoks.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always; 

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //_log.Debug("BeginRequest: " + Request.Url.ToString());
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //_log.Debug("Application_AuthenticateRequest");

            var accessToken = Request.Headers["AccessToken"];

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return;
            }

            var fb = new FacebookClient(accessToken);
            dynamic me = fb.Get("me"); // TODO: Add exception handling for renewing old tokens.
            long facebookUserId = long.Parse(me.id);

            var userId = CreateUserIfNotExists(facebookUserId, me, accessToken);
            var principal = new FacebookPrincipal(userId, facebookUserId, me.name, me.email, me.link, accessToken);

            Context.User = principal;

            _log.Info("User Logged On: " + facebookUserId);
        }

        private long CreateUserIfNotExists(long id, dynamic me, string token)
        {
            using (DataContext db = new DataContext())
            {
                var dbUser = db.Users.SingleOrDefault(u => u.FacebookId == id);

                if (dbUser == null)
                {
                    _log.Info("Creating User: " + id + " - " + me.name);

                    dbUser = new Models.User();
                    dbUser.FacebookId = id;
                    dbUser.Name = me.name;
                    dbUser.Email = me.email;
                    dbUser.Link = me.link;
                    dbUser.Token = token;

                    db.Users.Add(dbUser);
                    db.SaveChanges();
                }

                return dbUser.Id;
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            _log.ErrorException("Global Application Error", ex);
        }
    }
}