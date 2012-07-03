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

        public DbSet<Log> Logs { get; set; }

        public DbSet<Job> Jobs { get; set; }

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
        }
    }
}
