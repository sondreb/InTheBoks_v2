namespace InTheBoks.Handlers.Item
{
    using InTheBoks.Command;
    using InTheBoks.Commands;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CreateOrUpdateItemHandler : ICommandHandler<CreateOrUpdateItemCommand>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrUpdateItemHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(CreateOrUpdateItemCommand command)
        {
            var item = command.Item;

            if (item.Id == 0)
            {
                _itemRepository.Add(item);
            }
            else
            {
                var dbItem = _itemRepository.GetById(item.Id);

                // TODO: This should be auto-mapped.
                dbItem.ASIN = item.ASIN;
                dbItem.ImageUrl = item.ImageUrl;
                dbItem.Title = item.Title;
                dbItem.Url = item.Url;

                _itemRepository.Update(dbItem);
            }

            _unitOfWork.Commit();

            return new CommandResult(true);
        }
    }
}
