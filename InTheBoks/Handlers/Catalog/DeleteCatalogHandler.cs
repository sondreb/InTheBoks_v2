namespace InTheBoks.Handlers.Item
{
    using InTheBoks.Command;
    using InTheBoks.Commands;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Dispatcher;

    public class DeleteCatalogHandler : ICommandHandler<DeleteCatalogCommand>
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandBus _commandBus;

        public DeleteCatalogHandler(ICatalogRepository catalogRepository, IUnitOfWork unitOfWork, ICommandBus commandBus)
        {
            _catalogRepository = catalogRepository;
            _unitOfWork = unitOfWork;
            _commandBus = commandBus;
        }

        public ICommandResult Execute(DeleteCatalogCommand command)
        {
            // When we arrive here, the permission control on the item should already have been performed.
            var catalog = _catalogRepository.GetById(command.CatalogId);
            _catalogRepository.Delete(catalog);
            _unitOfWork.Commit();

            return new CommandResult(true);
        }
    }
}
