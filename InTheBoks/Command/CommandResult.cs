namespace InTheBoks.Command
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool success)
        {
            Success = success;
        }

        public CommandResult(bool success, long id)
        {
            Success = success;
            Id = id;
        }

        public bool Success { get; protected set; }

        public long Id { get; protected set; }
    }
}

