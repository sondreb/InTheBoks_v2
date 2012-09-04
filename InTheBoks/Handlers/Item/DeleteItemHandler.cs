namespace InTheBoks.Handlers.Item
{
    using InTheBoks.Command;
    using InTheBoks.Commands;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Dispatcher;

    public class DeleteItemHandler : ICommandHandler<DeleteItemCommand>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandBus _commandBus;

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
            _itemRepository.Delete(item);
            _unitOfWork.Commit();

            return new CommandResult(true);
        }
    }
}
