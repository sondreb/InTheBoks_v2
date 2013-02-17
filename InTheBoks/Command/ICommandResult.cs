namespace InTheBoks.Command
{
    public interface ICommandResult
    {
        long Id { get; }

        bool Success { get; }

        object Entity { get; }
    }
}