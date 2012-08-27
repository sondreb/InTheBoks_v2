namespace InTheBoks.Web.Api
{
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Models;
    using InTheBoks.Security;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using InTheBoks.Commands;
    using InTheBoks.Dispatcher;

    public class ItemsController : ApiController
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        private readonly IItemRepository _itemRepository;
        private readonly ICommandBus _commandBus;

        public ItemsController(IItemRepository itemRepository, ICommandBus commandBus)
        {
            _itemRepository = itemRepository;
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

                throw new ItemNotFoundException();
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
    }
}