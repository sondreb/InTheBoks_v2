namespace InTheBoks.Handlers
{
    using InTheBoks.Command;
    using InTheBoks.Commands;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Dispatcher;
    using InTheBoks.Models;
    using System;

    public class CreateOrUpdateCatalogHandler : ICommandHandler<CreateOrUpdateCatalogCommand>
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly ICommandBus _commandBus;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrUpdateCatalogHandler(ICatalogRepository catalogRepository, IUnitOfWork unitOfWork, ICommandBus commandBus)
        {
            _catalogRepository = catalogRepository;
            _unitOfWork = unitOfWork;
            _commandBus = commandBus;
        }

        public ICommandResult Execute(CreateOrUpdateCatalogCommand command)
        {
            Catalog catalog;

            if (command.CatalogId == -1) // TODO: Figure out why -1?!
            {
                catalog = new Catalog();
                catalog.Created = DateTime.UtcNow;
                catalog.Modified = DateTime.UtcNow;
                catalog.Name = command.Name;
                catalog.User_Id = command.UserId;
                catalog.Visibility = command.Visibility;
                _catalogRepository.Add(catalog);
            }
            else
            {
                catalog = _catalogRepository.GetById(command.CatalogId);

                if (catalog == null)
                {
                    return new CommandResult(false);
                }

                // TODO: This should be auto-mapped.
                catalog.Name = command.Name;
                catalog.Visibility = command.Visibility;
                catalog.Modified = DateTime.UtcNow;

                _catalogRepository.Update(catalog);
            }

            _unitOfWork.Commit();

            return new CommandResult(true, catalog.Id);
        }
    }
}