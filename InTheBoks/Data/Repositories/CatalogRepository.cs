namespace InTheBoks.Data.Repositories
{
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Models;

    public interface ICatalogRepository : IRepository<Catalog>
    {
    }

    public class CatalogRepository : RepositoryBase<Catalog>, ICatalogRepository
    {
        public CatalogRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}