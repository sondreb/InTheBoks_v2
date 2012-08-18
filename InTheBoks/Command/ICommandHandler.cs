namespace InTheBoks.Command
{
    public interface ICommandHandler<in TCommand> where TCommand: ICommand
    {
        ICommandResult Execute(TCommand command);
    }
}

