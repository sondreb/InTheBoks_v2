namespace InTheBoks.Dispatcher
{
    using System.Collections.Generic;
    using InTheBoks.Command;
    using InTheBoks.Common;

    public interface ICommandBus
    {
        ICommandResult Submit<TCommand>(TCommand command) where TCommand: ICommand;
        IEnumerable<ValidationResult> Validate<TCommand>(TCommand command) where TCommand : ICommand;
    }
}

