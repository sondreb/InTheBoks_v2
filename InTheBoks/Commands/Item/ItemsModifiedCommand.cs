using InTheBoks.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InTheBoks.Commands
{
    public class ItemsModifiedCommand : ICommand
    {
        public long CatalogId { get; set; }
    }
}
