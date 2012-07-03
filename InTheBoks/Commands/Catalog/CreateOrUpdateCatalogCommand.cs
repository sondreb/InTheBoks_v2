namespace InTheBoks.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using InTheBoks.Command;

    public class CreateOrUpdateCatalogCommand : ICommand
    {
        public CreateOrUpdateCatalogCommand(long catalogId, string name)
        {
            CatalogId = catalogId;
            Name = name;
        }

        public long CatalogId { get; set; }

        public string Name { get; set; }

    }
}
