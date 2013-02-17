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

        public CommandResult(bool success, long id, dynamic entity)
        {
            Success = success;
            Id = id;
            Entity = entity;
        }

        public long Id { get; protected set; }

        public bool Success { get; protected set; }

        public dynamic Entity { get; protected set; }
    }
}