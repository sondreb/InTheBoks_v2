namespace InTheBoks.Commands
{
    using InTheBoks.Command;

    public class ActivityCommand : ICommand
    {
        public ActivityCommand(long userId, long itemId, string statusText)
        {
            UserId = userId;
            ItemId = itemId;
            StatusText = statusText;
        }

        public long ItemId { get; set; }

        public string StatusText { get; set; }

        public long UserId { get; set; }
    }
}