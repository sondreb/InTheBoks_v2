using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using InTheBoks.Models;

namespace InTheBoks.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("InTheBoks")
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Catalog> Catalogs { get; set; }
    }
}
