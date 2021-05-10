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
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<DataContext>());
        }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
    } 
}

