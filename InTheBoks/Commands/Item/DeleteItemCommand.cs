namespace InTheBoks.Commands
{
    using InTheBoks.Command;

    public class DeleteItemCommand : ICommand
    {
        public DeleteItemCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}