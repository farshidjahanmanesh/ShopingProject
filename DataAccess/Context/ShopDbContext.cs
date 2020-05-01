using EntityModels.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public DbSet<Post> Post { get; set; }
        public DbSet<PostComment> PostComment { get; set; } = null;

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();

            #region added

            var AddedPosts = ChangeTracker.Entries<Post>().Where(x => x.State == EntityState.Added)
                .Select(x => x.Entity).ToArray();

            foreach (var item in AddedPosts)
            {
                item.PublishDate = DateTime.Now;
            }


            #endregion

            #region modified
            var posts = ChangeTracker.Entries<Post>()
                .Where(x => x.State == EntityState.Modified)
                .Select(x => x.Entity).ToArray();

            foreach (var item in posts)
            {
                item.Slug = item.Title.Trim().Replace(" ", "-");
            }


            #endregion

            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.ChangeTracker.DetectChanges();
            #region added

            var AddedPosts = ChangeTracker.Entries<Post>().Where(x => x.State == EntityState.Added)
                .Select(x=>x.Entity).ToArray();

            foreach(var item in AddedPosts)
            {
                item.PublishDate = DateTime.Now;
            }


            #endregion


            #region modified
            var modifiedposts = ChangeTracker.Entries<Post>()
                .Where(x => x.State == EntityState.Modified)
                .Select(x => x.Entity).ToArray();

            foreach (var item in modifiedposts)
            {
                if (item.Title != null && item.Title != "")
                    item.Slug = item.Title.Trim().Replace(" ", "-");

            }

            #endregion

            return base.SaveChangesAsync(cancellationToken);
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
