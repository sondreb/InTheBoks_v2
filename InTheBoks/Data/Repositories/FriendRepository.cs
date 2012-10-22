namespace InTheBoks.Data.Repositories
{
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Models;

    public interface IFriendRepository : IRepository<Friend>
    {
    }

    public class FriendRepository : RepositoryBase<Friend>, IFriendRepository
    {
        public FriendRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}