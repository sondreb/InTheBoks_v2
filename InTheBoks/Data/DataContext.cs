namespace InTheBoks.Data
{
    using System.Data.Entity;
    using InTheBoks.Models;

    public class DataContext : DbContext
    {
        public DataContext()
            : base("InTheBoks")
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Catalog> Catalogs { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<Friend> Friends { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Log>()
                .Property(f => f.Timestamp)
                .HasColumnType("datetime2")
                .HasPrecision(4);

            // Remove the cascade delete or else we won't be able to build the model from our code.
            modelBuilder.Entity<Item>()
            .HasRequired(i => i.User)
            .WithMany()
            .WillCascadeOnDelete(false);


            //modelBuilder.Entity<User>().Property(p => p.FacebookId).IsRequired();
        }

        public class Initializer : DropCreateDatabaseIfModelChanges<DataContext>
        {
            protected override void Seed(DataContext context)
            {
                context.Database.ExecuteSqlCommand("CREATE INDEX IX_User_FacebookId ON User (FacebookId)");

                base.Seed(context);
            }

            //public void InitializeDatabase(DataContext context)
            //{
            //    if (!context.Database.Exists() || !context.Database.CompatibleWithModel(false))
            //    {
            //        context.Database.Delete();
            //        context.Database.Create();

                    
            //    }
            //}
        }
    }
}
