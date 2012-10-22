namespace InTheBoks.Handlers
{
    using InTheBoks.Command;
    using InTheBoks.Commands;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using System;
    using System.Linq;

    public class ItemsModifiedHandler : ICommandHandler<ItemsModifiedCommand>
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ItemsModifiedHandler(ICatalogRepository catalogRepository, IItemRepository itemRepository, IUnitOfWork unitOfWork)
        {
            _catalogRepository = catalogRepository;
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(ItemsModifiedCommand command)
        {
            var catalog = _catalogRepository.GetById(command.CatalogId);

            if (catalog == null) // This should never really happen...
            {
                return new CommandResult(false);
            }

            catalog.Count = _itemRepository.Query().Where(i => i.Catalog_Id == catalog.Id).Count();
            catalog.Modified = DateTime.UtcNow;

            _catalogRepository.Update(catalog);
            _unitOfWork.Commit();

            return new CommandResult(true);
        }
    }
}