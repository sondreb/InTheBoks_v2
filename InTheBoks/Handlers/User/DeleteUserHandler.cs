namespace InTheBoks.Handlers
{
    using InTheBoks.Command;
    using InTheBoks.Commands;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Dispatcher;
    using NLog;

    public class DeleteUserHandler : ICommandHandler<DeleteUserCommand>
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly ICommandBus _commandBus;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ICommandBus commandBus)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _commandBus = commandBus;
        }

        public ICommandResult Execute(DeleteUserCommand command)
        {
            var user = _userRepository.GetById(command.Id);

            if (user == null) // Trying to delete something that is already gone, or does not exists.
            {
                _log.Error("Someone is trying to delete a user that does not exists. ID: " + command.Id);
                return new CommandResult(false);
            }

            _userRepository.Delete(user);
            _unitOfWork.Commit();

            return new CommandResult(true);
        }
    }
}