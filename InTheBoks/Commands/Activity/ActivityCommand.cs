namespace InTheBoks.Commands
{
    using InTheBoks.Command;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActivityCommand : ICommand
    {
        public ActivityCommand(long userId, long itemId, string statusText)
        {
            UserId = userId;
            ItemId = itemId;
            StatusText = statusText;
        }

        public long UserId { get; set; }

        public long ItemId { get; set; }

        public string StatusText { get; set; }
    }
}
