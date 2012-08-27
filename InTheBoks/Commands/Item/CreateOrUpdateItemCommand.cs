using InTheBoks.Command;
using InTheBoks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InTheBoks.Commands
{
    public class CreateOrUpdateItemCommand : ICommand
    {
        public CreateOrUpdateItemCommand(Item item)
        {
            Item = item;
        }

        public Item Item { get; set; }
    }
}
