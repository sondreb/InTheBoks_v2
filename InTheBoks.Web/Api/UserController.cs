namespace InTheBoks.Web.Api
{
    using InTheBoks.Data.Repositories;
    using InTheBoks.Models;
    using InTheBoks.Security;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class UserController : ApiController
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Get()
        {
            var identity = (FacebookIdentity)User.Identity;
            var user = _userRepository.GetById(identity.Id);

            return user;
        }

        public User Put(long id, [FromBody]User item)
        {
            var identity = (FacebookIdentity)User.Identity;

            if (identity.Id != item.Id)
            {
                throw new ItemNotFoundException();
            }

            var user = _userRepository.GetById(identity.Id);
            return user;
        }
    }
}
