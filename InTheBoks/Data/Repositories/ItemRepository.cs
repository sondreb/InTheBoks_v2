namespace InTheBoks.Data.Repositories
{
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Models;

    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IItemRepository : IRepository<Item>
    {
    }
}
