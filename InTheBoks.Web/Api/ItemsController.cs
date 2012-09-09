namespace InTheBoks.Web.Api
{
    using InTheBoks.Commands;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Dispatcher;
    using InTheBoks.Models;
    using InTheBoks.Security;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    public class ItemsController : ApiController
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        private readonly IItemRepository _itemRepository;
        private readonly ICatalogRepository _catalogRepository;
        private readonly ICommandBus _commandBus;

        public ItemsController(IItemRepository itemRepository, ICatalogRepository catalogRepository, ICommandBus commandBus)
        {
            _itemRepository = itemRepository;
            _catalogRepository = catalogRepository;
            _commandBus = commandBus;
        }

        // UPDATE
        public Item Put(int id, [FromBody]Item item)
        {
            var user = (FacebookIdentity)User.Identity;

            if (item.Id == 0)
            {
                throw new InvalidOperationException("Put should be used for updating items. Use the Post method for creations.");
            }

            var dbItem = _itemRepository.Query().Where(i => i.Id == item.Id && i.User_Id == user.Id);

            if (dbItem == null)
            {
                // This probably means someone is trying to update someone elses item. Let's verify so we can log
                // all attempts to gain illicit access.

                var existsOnAnotherUser = _itemRepository.Query().Where(i => i.Id == item.Id).Any();

                if (existsOnAnotherUser)
                {
                    _log.Fatal("Someone is trying to update another user's item. User ID: " + user.Id + " Item ID: " + item.Id);
                }
                else
                {
                    _log.Error("User is trying to access item that does not exists. User ID: " + user.Id + " Item ID: " + item.Id);
                }

                throw new ItemNotFoundException("Item does not exists.");
            }

            return item;  
        }

        // CREATE
        public Item Post(Item item)
        {
            var user = (FacebookIdentity)User.Identity;

            if (item.Id != 0)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, "Post should be used for new items. Use the Put method for updates.");
                throw new InvalidOperationException("Post should be used for new items. Use the Put method for updates.");
            }

            // Verify if the user actually owns the specified catalog.
            var catalogExists = _catalogRepository.Query().Where(c => c.User_Id == user.Id && c.Id == item.Catalog_Id).Any();

            if (!catalogExists)
            {
                _log.Fatal("Someone is trying to insert item on invalid catalog, perhaps another user's. User ID: " + user.Id + " Catalog ID: " + item.Catalog_Id);
                throw new ItemNotFoundException("Catalog does not exists.");
            }

            item.User_Id = user.Id;

            CreateOrUpdateItemCommand cmd = new CreateOrUpdateItemCommand(item);
            var result = _commandBus.Submit(cmd);

            if (result.Success)
            {
                return item;
            }
            else
            {
                throw new ApplicationException("Unable to save item");
            }
        }

        // GET
        public IEnumerable<Item> Get(long id)
        {
            var user = (FacebookIdentity)User.Identity;
            var query = _itemRepository.Query().Where(c => c.User_Id == user.Id && c.Catalog_Id == id);
            return query.ToList();
        }

        public void Delete(int id)
        {
            var user = (FacebookIdentity)User.Identity;

            var dbItem = _itemRepository.GetById(id);

            if (dbItem == null)
            {
                throw new ItemNotFoundException("Item does not exists.");
            }

            if (dbItem.User_Id != user.Id)
            {
                _log.Warn("Someone is potentially trying to delete another users item. User ID: " + user.Id);
                throw new ItemNotFoundException("Item does not exists.");
            }

            // Submit a delete operation to any handlers.
            _commandBus.Submit(new DeleteItemCommand(dbItem.Id));
        }
    }
}