namespace InTheBoks.Web.Api
{
    using InTheBoks.Commands;
    using InTheBoks.Dispatcher;
    using InTheBoks.Security;
    using NLog;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;

    [Authorize]
    public class AccountController : ApiController
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();

        private readonly ICommandBus _commandBus;

        public AccountController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public void Delete()
        {
            var user = (FacebookIdentity)User.Identity;

            var ipAddress = GetClientIp(Request);

            _log.Info(string.Format("{0} (ID: {1} - FB: {2}) requested to be deleted. Email: {3}. IP address: {4}. Link: {5}. Proceeding to delete all his data and account.", user.Name, user.Id, user.FacebookId, user.Email, ipAddress, user.Link));

            _commandBus.Submit(new DeleteUserCommand(user.Id));
        }

        private string GetClientIp(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }
    }
}