using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyTrendyol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrendyol.Contexts
{
    public class TrendyolDbContext : DbContext
    {
        public TrendyolDbContext()
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductsForOrder> ProductsForOrders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WareHouse> WareHouse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(builder.GetConnectionString("Default"));
            }
        }

    }
}
