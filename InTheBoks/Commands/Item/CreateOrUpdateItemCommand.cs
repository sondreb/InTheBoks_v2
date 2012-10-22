namespace InTheBoks.Commands
{
    using InTheBoks.Command;
    using InTheBoks.Models;

    public class CreateOrUpdateItemCommand : ICommand
    {
        public CreateOrUpdateItemCommand(Item item)
        {
            Item = item;
        }

        public Item Item { get; set; }
    }
}