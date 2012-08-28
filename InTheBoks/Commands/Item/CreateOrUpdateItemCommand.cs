namespace InTheBoks.Commands
{
    using InTheBoks.Command;
    using InTheBoks.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CreateOrUpdateItemCommand : ICommand
    {
        public CreateOrUpdateItemCommand(Item item)
        {
            Item = item;
        }

        public Item Item { get; set; }
    }
}
