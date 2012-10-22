namespace InTheBoks.Data.Repositories
{
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Models;

    public interface IActivityRepository : IRepository<Activity>
    {
    }

    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
    {
        public ActivityRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}