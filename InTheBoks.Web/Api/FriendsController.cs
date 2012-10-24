namespace InTheBoks.Web.Api
{
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Models;
    using InTheBoks.Security;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    [Authorize]
    public class FriendsController : ApiController
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public FriendsController(IUserRepository userRepository, IFriendRepository friendRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _friendRepository = friendRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<dynamic> Get()
        {
            var user = (FacebookIdentity)User.Identity;
            var friends = _friendRepository.Query().SingleOrDefault(f => f.User_Id == user.Id);

            if (friends == null)
            {
                return new List<dynamic>();
            }

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