namespace InTheBoks.Command
{
    public interface ICommandSuccessHandler<in TCommand> where TCommand : ICommand
    {
        void Notify(ICommandResult result);
    }
}
