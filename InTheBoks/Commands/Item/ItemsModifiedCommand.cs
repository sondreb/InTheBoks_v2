using InTheBoks.Command;

namespace InTheBoks.Commands
{
    public class ItemsModifiedCommand : ICommand
    {
        public long CatalogId { get; set; }
    }
}