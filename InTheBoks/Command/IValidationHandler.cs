namespace InTheBoks.Command
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using InTheBoks.Common;

    public interface IValidationHandler<in TCommand> where TCommand : ICommand
    {
        IEnumerable<ValidationResult>  Validate(TCommand command);
    }
}
