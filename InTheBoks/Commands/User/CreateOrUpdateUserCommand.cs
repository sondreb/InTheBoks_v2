namespace InTheBoks.Commands
{
    using InTheBoks.Command;
    using InTheBoks.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CreateOrUpdateUserCommand : ICommand
    {
        public CreateOrUpdateUserCommand(User user)
        {
            User = user;
        }

        public User User { get; private set; }
    }
}
