namespace InTheBoks.Data.Repositories
{
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Models;

    public class JobRepository : RepositoryBase<Job>, IJobRepository
    {
        public JobRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IJobRepository : IRepository<Job>
    {
    }
}
