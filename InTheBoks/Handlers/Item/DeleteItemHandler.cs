namespace InTheBoks.Handlers
{
    using InTheBoks.Command;
    using InTheBoks.Commands;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Dispatcher;

    public class DeleteItemHandler : ICommandHandler<DeleteItemCommand>
    {
        private readonly ICommandBus _commandBus;
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteItemHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork, ICommandBus commandBus)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
            _commandBus = commandBus;
        }

        public ICommandResult Execute(DeleteItemCommand command)
        {
            // When we arrive here, the permission control on the item should already have been performed.

            var item = _itemRepository.GetById(command.Id);
            var catalogId = item.Catalog_Id;

            _itemRepository.Delete(item);
            _unitOfWork.Commit();

            // Recalculate the total amount of items in the catalog.
            ItemsModifiedCommand modifiedcmd = new ItemsModifiedCommand() { CatalogId = catalogId };
            _commandBus.Submit(modifiedcmd);

            return new CommandResult(true);
        }
    }
}