using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataAccess.Context
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext()
        {

        }
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {

        }
        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server = .; Database = MyShopTestDb; Trusted_Connection = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Assembly EntityModelAssembly = Assembly.Load("EntityModels");
            modelBuilder.ApplyConfigurationsFromAssembly(EntityModelAssembly);
        }

    }
}
