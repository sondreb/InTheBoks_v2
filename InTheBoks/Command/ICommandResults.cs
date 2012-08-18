namespace InTheBoks.Command
{
    public interface ICommandResults
    {
        ICommandResult[] Results { get; }

        bool Success { get; }
    }
}

