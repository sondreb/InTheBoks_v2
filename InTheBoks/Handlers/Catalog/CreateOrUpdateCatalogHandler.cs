namespace InTheBoks.Handlers.Item
{
    using InTheBoks.Command;
    using InTheBoks.Commands;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Dispatcher;
    using InTheBoks.Models;

    public class CreateOrUpdateCatalogHandler : ICommandHandler<CreateOrUpdateCatalogCommand>
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandBus _commandBus;

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
                catalog.Name = command.Name;
                catalog.User_Id = command.UserId;
                _catalogRepository.Add(catalog);
            }
            else
            {
                var dbItem = _catalogRepository.GetById(command.CatalogId);

                if (dbItem == null)
                {
                    return new CommandResult(false);
                }

                // TODO: This should be auto-mapped.
                dbItem.Name = command.Name;

                _catalogRepository.Update(dbItem);
            }

            _unitOfWork.Commit();

            return new CommandResult(true);
        }
    }
}
