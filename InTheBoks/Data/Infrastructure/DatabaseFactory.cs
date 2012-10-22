namespace InTheBoks.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private DataContext dataContext;

        public DataContext Get()
        {
            return dataContext ?? (dataContext = new DataContext());
        }

        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}