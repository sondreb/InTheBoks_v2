namespace InTheBoks.Handlers.Item
{
    using InTheBoks.Command;
    using InTheBoks.Commands;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Dispatcher;
    using System;
    using System.Linq;

    public class CreateOrUpdateUserHandler : ICommandHandler<CreateOrUpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandBus _commandBus;

        public CreateOrUpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ICommandBus commandBus)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _commandBus = commandBus;
        }

        public ICommandResult Execute(CreateOrUpdateUserCommand command)
        {
            var user = command.User;

            var dbUser = _userRepository.Query().SingleOrDefault(u => u.FacebookId == user.FacebookId);

            if (dbUser == null)
            {
                //_log.Info("Creating User: " + id + " - " + me.name);

                dbUser = new Models.User();
                dbUser.Created = DateTime.UtcNow;
                dbUser.Modified = DateTime.UtcNow;
                dbUser.FacebookId = user.FacebookId;
                dbUser.Name = user.Name;
                dbUser.Email = user.Email;
                dbUser.Link = user.Link;
                dbUser.Token = user.Token;
                dbUser.TokenExpire = user.TokenExpire;
                dbUser.ShareActivity = user.ShareActivity; // We share activity inside InTheBoks by default.
                dbUser.ShareFacebook = user.ShareFacebook; // We don't share to Facebook by default.

                _userRepository.Add(dbUser);
            }
            else
            {
                //_log.Info("Updating User: " + id + " - " + me.name);
                dbUser.Modified = DateTime.UtcNow;
                //dbUser.Name = user.Name;
                //dbUser.Email = user.Email;
                //dbUser.Link = user.Link;
                //dbUser.Token = user.Token;
                //dbUser.TokenExpire = user.TokenExpire;
                dbUser.ShareActivity = user.ShareActivity;
                dbUser.ShareFacebook = user.ShareFacebook;
                dbUser.Language = user.Language;
                dbUser.TimeZone = user.TimeZone;

                _userRepository.Update(dbUser);
            }

            _unitOfWork.Commit();

            return new CommandResult(true);
        }
    }
}
