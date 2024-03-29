﻿namespace InTheBoks.Web
{
    using Facebook;
    using InTheBoks.Data;
    using InTheBoks.Models;
    using InTheBoks.Security;
    using Microsoft.AspNet.SignalR;
    using NLog;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.Cookies["AccessToken"] == null)
            {
                return;
            }

            var accessToken = Request.Cookies["AccessToken"].Value;
            var expiresIn = Request.Cookies["AccessTokenExpiresIn"].Value;

            //var accessToken = Request.Headers["AccessToken"];

            if (string.IsNullOrWhiteSpace(accessToken) || accessToken == "null")
            {
                return;
            }

            int accessTokenExpiresIn;
            int.TryParse(expiresIn, out accessTokenExpiresIn);
            DateTime tokenExpireDate = DateTime.UtcNow + TimeSpan.FromSeconds(accessTokenExpiresIn);

            var fb = new FacebookClient(accessToken);

            dynamic me = null;

            try
            {
                me = fb.Get("me"); // TODO: Add exception handling for renewing old tokens.
            }
            catch (FacebookApiException fex)
            {
                if (fex.ErrorCode == 190)
                {
                    //{"(OAuthException - #190) Error validating access token: Session has expired at unix time 1361142000. The current unix time is 1361143214."}
                    throw new TokenExpiredException("Token has expired, please renew.");
                }
            }

            long facebookUserId = long.Parse(me.id);

            try
            {
                var user = CreateUserIfNotExists(facebookUserId, me, accessToken, tokenExpireDate);
                var principal = new FacebookPrincipal(user.Id, facebookUserId, me.name, me.email, me.link, accessToken, user.Language, user.TimeZone);
                Context.User = principal;
                _log.Info("User Logged On: " + facebookUserId);
            }
            catch (Exception ex)
            {
                _log.ErrorException("Failed while create/verify user.", ex);
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //_log.Debug("BeginRequest: " + Request.Url.ToString());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            _log.ErrorException("Global Application Error", ex);
        }

        protected void Application_Start()
        {
            // IoC registration
            ContainerConfig.RegisterContainer(GlobalConfiguration.Configuration);

            // Register the default hubs route: ~/signalr, has to happen before other routes.
            RouteTable.Routes.MapHubs();

            MvcHandler.DisableMvcResponseHeader = true;
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            FilterConfig.RegisterHttpFilters(GlobalConfiguration.Configuration.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        private User CreateUserIfNotExists(long id, dynamic me, string token, DateTime expireDate)
        {
            using (DataContext db = new DataContext())
            {
                lock (db) // Trying to see if this fixes race problems on initial user creation.
                {
                    var dbUser = db.Users.SingleOrDefault(u => u.FacebookId == id);

                    if (dbUser == null)
                    {
                        _log.Info("Creating User: " + id + " - " + me.name);

                        dbUser = new Models.User();
                        dbUser.Created = DateTime.UtcNow;
                        dbUser.FacebookId = id;
                        dbUser.Name = me.name;
                        dbUser.Email = me.email;
                        dbUser.Link = me.link;
                        dbUser.Token = token;
                        dbUser.TokenExpire = expireDate;
                        dbUser.ShareActivity = true; // We share activity inside InTheBoks by default.
                        dbUser.ShareFacebook = false; // We don't share to Facebook by default.

                        db.Users.Add(dbUser);
                        db.SaveChanges();
                    }
                    else
                    {
                        _log.Info("Updating User: " + id + " - " + me.name);

                        dbUser.Name = me.name;
                        dbUser.Email = me.email;
                        dbUser.Link = me.link;
                        dbUser.Token = token;
                        dbUser.TokenExpire = expireDate;

                        db.SaveChanges();
                    }

                    return dbUser;
                }
            }
        }
    }
}