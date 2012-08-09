namespace InTheBoks.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Models;

    public class FriendRepository : RepositoryBase<Friend>, IFriendRepository
    {
        public FriendRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IFriendRepository : IRepository<Friend>
    {
    }
}
