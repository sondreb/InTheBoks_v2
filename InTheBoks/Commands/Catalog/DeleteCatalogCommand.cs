namespace InTheBoks.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using InTheBoks.Command;

    public class DeleteCatalogCommand : ICommand
    {
        public DeleteCatalogCommand(long catalogId)
        {
            CatalogId = catalogId;
        }

        public long CatalogId { get; set; }
    }
}
