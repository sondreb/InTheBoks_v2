namespace InTheBoks.Commands
{
    using InTheBoks.Command;
    using InTheBoks.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DeleteItemCommand : ICommand
    {
        public DeleteItemCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
