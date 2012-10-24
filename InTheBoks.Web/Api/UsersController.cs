namespace InTheBoks.Web.Api
{
    using InTheBoks.Commands;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Dispatcher;
    using InTheBoks.Models;
    using InTheBoks.Security;
    using NLog;
    using System.Web.Http;

    [Authorize]
    public class UsersController : ApiController
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly ICommandBus _commandBus;
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository, ICommandBus commandBus)
        {
            _userRepository = userRepository;
            _commandBus = commandBus;
        }

        public User Get()
        {
            var identity = (FacebookIdentity)User.Identity;
            var user = _userRepository.GetById(identity.Id);

            if (string.IsNullOrEmpty(user.TimeZone))
            {
                user.TimeZone = "UTC";
            }

            return user;
        }

        public User Put([FromBody]User user)
        {
            var identity = (FacebookIdentity)User.Identity;

            if (identity.Id != user.Id)
            {
                throw new ItemNotFoundException();
            }

            // We will only allow a few specific properties to be updated in this api controller.
            var userTmp = new User();
            userTmp.Id = identity.Id;
            userTmp.FacebookId = identity.FacebookId;
            userTmp.ShareActivity = user.ShareActivity;
            userTmp.ShareFacebook = user.ShareFacebook;
            userTmp.Language = user.Language;
            userTmp.TimeZone = user.TimeZone;

            CreateOrUpdateUserCommand cmd = new CreateOrUpdateUserCommand(userTmp);
            var results = _commandBus.Submit(cmd);

            if (results.Success)
            {
                user = _userRepository.GetById(identity.Id);
                return user;
            }
            else
            {
                throw new ItemNotFoundException("Unable to update user.");
            }
        }
    }
}