namespace InTheBoks.Handlers
{
    using InTheBoks.Command;
    using InTheBoks.Commands;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using System;

    public class ActivityHandler : ICommandHandler<ActivityCommand>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivityHandler(IActivityRepository activityRepository, IUnitOfWork unitOfWork)
        {
            _activityRepository = activityRepository;
            _unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(ActivityCommand command)
        {
            var activity = new Models.Activity();

            activity.Created = DateTime.UtcNow;
            activity.Item_Id = command.ItemId;
            activity.User_Id = command.UserId;
            activity.StatusText = command.StatusText;

            _activityRepository.Add(activity);
            _unitOfWork.Commit();

            return new CommandResult(true);
        }
    }
}