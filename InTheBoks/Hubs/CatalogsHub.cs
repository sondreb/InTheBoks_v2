namespace InTheBoks.Hubs
{
    using InTheBoks.Command;
    using InTheBoks.Commands;
    using InTheBoks.Data.Repositories;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    [Authorize]
    [HubName("catalogs")]
    public class CatalogsHub : BaseHub, ICommandSuccessHandler<CreateOrUpdateCatalogCommand>
    {
        public CatalogsHub(ICatalogRepository repository)
        {

        }

        public void Notify(ICommandResult result)
        {
            //var context = GlobalHost.ConnectionManager.GetHubContext<CatalogsHub>();

            Clients.OthersInGroup(UserId).catalog(result.Entity);
        }
    }
}