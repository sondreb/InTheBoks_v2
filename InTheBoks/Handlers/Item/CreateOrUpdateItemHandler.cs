﻿namespace InTheBoks.Handlers.Item
{
    using InTheBoks.Command;
    using InTheBoks.Commands;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Dispatcher;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CreateOrUpdateItemHandler : ICommandHandler<CreateOrUpdateItemCommand>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandBus _commandBus;

        public CreateOrUpdateItemHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork, ICommandBus commandBus)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
            _commandBus = commandBus;
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

            // Recalculate the total amount of items in the catalog.
            ItemsModifiedCommand modifiedcmd = new ItemsModifiedCommand() { CatalogId = item.Catalog_Id };
            _commandBus.Submit(modifiedcmd);

            return new CommandResult(true);
        }
    }
}
