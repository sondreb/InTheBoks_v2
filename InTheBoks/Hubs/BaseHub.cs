namespace InTheBoks.Hubs
{
    using InTheBoks.Security;
    using Microsoft.AspNet.SignalR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;

    [Authorize]
    public abstract class BaseHub : Hub
    {
        public FacebookPrincipal User
        {
            get
            {
                // The Context is not set when this is run as a task/background thread.
                return (FacebookPrincipal)HttpContext.Current.User;
                //return (FacebookPrincipal)Context.User;
            }
        }

        public string UserId
        {
            get { return User.Id.ToString(); }
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            // We need to verify the safety of this method. Is it possible
            // for the user to later manipulate the group ID and have them
            // return data that does not belong to them?
            Groups.Add(Context.ConnectionId, UserId);

            return base.OnConnected();
        }
    }
}
