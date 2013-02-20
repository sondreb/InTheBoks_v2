namespace InTheBoks.Hubs
{
    using InTheBoks.Data.Repositories;
    using InTheBoks.Models;
    using InTheBoks.Security;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    [Authorize]
    [HubName("activities")]
    public class ActivitiesHub : BaseHub
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
            Clients.OthersInGroup(UserId).notify("Hello World");

            //Clients.Group(user.FacebookId.ToString()).notify("Hello World");
            //Clients.All.notify("Hello World");
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