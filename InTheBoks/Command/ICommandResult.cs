namespace InTheBoks.Command
{
    public interface ICommandResult
    {
        bool Success { get; }
        long Id { get; }
    }
}

