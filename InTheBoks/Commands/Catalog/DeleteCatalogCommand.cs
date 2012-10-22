namespace InTheBoks.Commands
{
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