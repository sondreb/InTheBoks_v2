namespace InTheBoks.Data.Repositories
{
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Models;

    public interface ILogRepository : IRepository<Log>
    {
    }

    public class LogRepository : RepositoryBase<Log>, ILogRepository
    {
        public LogRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}