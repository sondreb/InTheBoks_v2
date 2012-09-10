namespace InTheBoks.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using InTheBoks.Command;

    public class CreateOrUpdateCatalogCommand : ICommand
    {
        public CreateOrUpdateCatalogCommand(long catalogId, string name, long userId, Privacy visibility)
        {
            CatalogId = catalogId;
            Name = name;
            UserId = userId;
            Visibility = visibility;
        }

        public long CatalogId { get; set; }

        public string Name { get; set; }

        public long UserId { get; set; }

        public Privacy Visibility { get; set; }
    }
}
