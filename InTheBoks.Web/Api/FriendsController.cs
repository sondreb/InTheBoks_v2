namespace InTheBoks.Web.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Models;
    using InTheBoks.Security;

    public class FriendsController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IFriendRepository _friendRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FriendsController(IUserRepository userRepository, IFriendRepository friendRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _friendRepository = friendRepository;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public IEnumerable<dynamic> Get()
        {
            var user = (FacebookIdentity)User.Identity;
            var friends = _friendRepository.Query().SingleOrDefault(f => f.User_Id == user.Id);
            var friendsArray = friends.FriendIds.Split(';');

            var friendsArrayLong = friendsArray.Select(x => long.Parse(x)).ToArray();
            var users = _userRepository.Query()
                .Where(u => friendsArrayLong.Contains(u.Id))
                .Select(u => new
                    {
                        Id = u.Id,
                        Name = u.Name,
                        FacebookId = u.FacebookId
                    });

            return users.ToArray();
        }
    }
}
