namespace InTheBoks.Data.Repositories
{
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
    {
        public ActivityRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IActivityRepository : IRepository<Activity>
    {
    }
}
