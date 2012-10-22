using InTheBoks.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InTheBoks.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public DeleteUserCommand(long id)
        {
            Id = id;
        }

        public long Id { get; private set; }
    }
}