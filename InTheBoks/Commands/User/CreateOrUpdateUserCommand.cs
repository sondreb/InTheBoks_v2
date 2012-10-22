namespace InTheBoks.Commands
{
    using InTheBoks.Command;
    using InTheBoks.Models;

    public class CreateOrUpdateUserCommand : ICommand
    {
        public CreateOrUpdateUserCommand(User user)
        {
            User = user;
        }

        public User User { get; private set; }
    }
}