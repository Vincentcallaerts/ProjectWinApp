using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWinApp
{
    class DataContext : DbContext
    {
        public DataContext() : base("name = ConnectString")
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists<DataContext>());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
            Database.SetInitializer(new DropCreateDatabaseAlways<DataContext>());
        }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Magazijn> Magazijn { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductsMagazijn> ProductsMagazijn { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }


    }
}

