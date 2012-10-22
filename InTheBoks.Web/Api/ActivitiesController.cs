namespace InTheBoks.Web.Api
{
    using InTheBoks.Data.Repositories;
    using InTheBoks.Models;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    public class ActivitiesController : ApiController
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IActivityRepository _activityRepository;

        public ActivitiesController(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public IEnumerable<Activity> Get()
        {
            // TODO: Investigate the weird reason why the date in the query is
            // automatically converted from UTC to local time when it's not even parsed as a date.

            NameValueCollection queryString = HttpUtility.ParseQueryString(Request.RequestUri.Query);

            //var user = (FacebookIdentity)User.Identity;

            IQueryable<Activity> query = _activityRepository.Query().Include("User").Include("Item");

            // Happens on the first request.
            if (string.IsNullOrEmpty(queryString["date"]))
            {
                query = query.Take(30);
            }
            else // Sub-sequent requests.
            {
                var date = DateTime.ParseExact(queryString["date"],
                                       "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.AssumeUniversal);

                query = query.Where(a => a.Created >= date);
            }

            // TODO: Retreive only the activites of friends to the current user.
            return query.ToList();
        }
    }
}