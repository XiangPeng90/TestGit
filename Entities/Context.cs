using System;
using Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class Context : DbContext
    {
        public DbSet<Food>? Foods { get; set; }

        public DbSet<Menu>? Menus { get; set; }

        public DbSet<Order>? Orders { get; set; }

        public DbSet<Shop>? Shops { get; set; }

        public DbSet<User>? Users { get; set; }

        public DbSet<View>? Views { get; set; }

        public DbSet<Trolley>? Trolleys { get; set; }

        public DbSet<ShopInfo>? shopInfos { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}

