using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace OnlineShop.Models
{
    public class DatabaseContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Data Source=.\SQLEXPRESS;
                                   Initial Catalog=OnlineShopDb;
                                       Integrated Security=SSPI");

            //option.UseSqlServer(@"Data Source=.\SQLEXPRESS;
            //                       Initial Catalog=OnlineShopDbTmp2;
            //                           Integrated Security=SSPI");
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Factor> Factors { get; set; }
        public DbSet<FactorDetail> FactorDetails { get; set; }

    }
}
