namespace InTheBoks.Web.Hubs
{
    using InTheBoks.Data.Repositories;
    using InTheBoks.Models;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    [Authorize]
    [HubName("activities")]
    public class ActivitiesHub : Hub
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IActivityRepository _activityRepository;

        public ActivitiesHub()
        {

        }

        //public ActivitiesHub(IActivityRepository activityRepository)
        //{
        //    _activityRepository = activityRepository;
        //}

        public void NotifyClients()
        {
            Clients.All.notify("Hello World");
        }

        //public IEnumerable<Activity> GetAllActivities()
        //{
        //    if (!Context.User.Identity.IsAuthenticated)
        //    {
        //        throw new ApplicationException("No permission");
        //    }

        //    IQueryable<Activity> query = _activityRepository.Query().Include("User").Include("Item");

        //    return query.ToList();
        //}
    }
}