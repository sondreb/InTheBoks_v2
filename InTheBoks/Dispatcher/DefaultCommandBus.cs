namespace InTheBoks.Dispatcher
{
    using InTheBoks.Command;
    using InTheBoks.Common;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class DefaultCommandBus : ICommandBus
    {
        public ICommandResult Submit<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = DependencyResolver.Current.GetService<ICommandHandler<TCommand>>();
            if (!((handler != null) && handler is ICommandHandler<TCommand>))
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }

            var results = handler.Execute(command);

            // If the command was successfull, find the success handler and notify.
            if (results.Success)
            {
                //dynamic successHandler = Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver.GetService(typeof(ICommandSuccessHandler<TCommand>));
                //var successHandler = DependencyResolver.Current.GetService<ICommandSuccessHandler<TCommand>>();

                var successHandler = DependencyResolver.Current.GetService<InTheBoks.Hubs.CatalogsHub>();

                if (successHandler != null)
                {
                    successHandler.Notify(results);
                }
            }

            return results;
        }

        public IEnumerable<ValidationResult> Validate<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = DependencyResolver.Current.GetService<IValidationHandler<TCommand>>();
            if (!((handler != null) && handler is IValidationHandler<TCommand>))
            {
                throw new ValidationHandlerNotFoundException(typeof(TCommand));
            }
            return handler.Validate(command);
        }
    }
}