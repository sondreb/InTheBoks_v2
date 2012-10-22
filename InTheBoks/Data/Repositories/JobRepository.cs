namespace InTheBoks.Data.Repositories
{
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Models;

    public interface IJobRepository : IRepository<Job>
    {
    }

    public class JobRepository : RepositoryBase<Job>, IJobRepository
    {
        public JobRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}