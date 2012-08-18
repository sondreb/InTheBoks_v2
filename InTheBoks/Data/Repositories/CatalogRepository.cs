namespace InTheBoks.Data.Repositories
{
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Models;

    public class CatalogRepository : RepositoryBase<Catalog>, ICatalogRepository
    {
        public CatalogRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ICatalogRepository : IRepository<Catalog>
    {
    }
}
