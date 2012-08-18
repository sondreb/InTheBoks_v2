namespace InTheBoks.Data.Repositories
{
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Models;

    public class LogRepository : RepositoryBase<Log>, ILogRepository
    {
        public LogRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ILogRepository : IRepository<Log>
    {
    }
}
