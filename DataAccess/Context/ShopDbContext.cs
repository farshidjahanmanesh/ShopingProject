using EntityModels.Entities.BasicEntity;
using EntityModels.Entities.Categories;
using EntityModels.Entities.Posts;
using EntityModels.Entities.Products;
using EntityModels.Entities.Site;
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
        public DbSet<Product> Product { get; set; }
        public DbSet<PostComment> PostComment { get; set; }
        public DbSet<PostKeyword> PostKeyword { get; set; }
        public DbSet<ProductKeyword> ProductKeyword { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductGroups> ProductGroups { get; set; }
        public DbSet<ProductComment> ProductComment { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<NewsLetter> Newsletter { get; set; }
        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();

            #region added
            var AllEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).Select(x => x.Entity);
            
            foreach (var item in AllEntities)
            {
                if (item.GetType().GetInterface(nameof(IBase)) != null)
                {
                    IBase basicFile = item as IBase;
                    basicFile.IsActive = false;
                    basicFile.PublishDate = DateTime.Now;
                }
                else if (item.GetType().GetInterface(nameof(IIsActive)) != null)
                {
                    IIsActive ISAct = item as IIsActive;
                    ISAct.IsActive = false;
                }
            }
           

            #endregion

            #region modified
            var Modifiedobject = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified)
                .Select(x => x.Entity).ToArray();

            foreach (var item in Modifiedobject)
            {
                if (item.GetType().Name == nameof(EntityModels.Entities.Posts.Post))
                {
                    var SourceObject = item as Post;
                    SourceObject.Slug = SourceObject.Title.Trim().Replace(" ", "-");

                }else if(item.GetType().Name == nameof(EntityModels.Entities.Products.Product))
                {
                    var SourceObject = item as Product;
                    SourceObject.Slug = SourceObject.Name.Trim().Replace(" ", "-");
                }
            }


            #endregion

            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.ChangeTracker.DetectChanges();
            #region added
            var AllEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).Select(x => x.Entity);

            foreach (var item in AllEntities)
            {
                if (item.GetType().GetInterface(nameof(IBase))!=null)
                {
                    IBase basicFile = item as IBase;
                    basicFile.IsActive = false;
                    basicFile.PublishDate = DateTime.Now;
                }
                else if (item.GetType().GetInterface(nameof(IIsActive)) != null)
                {
                    IIsActive ISAct = item as IIsActive;
                    ISAct.IsActive = false;
                }
            }


            #endregion



            #region modified
            var Modifiedobject = ChangeTracker.Entries()
                  .Where(x => x.State == EntityState.Modified)
                  .Select(x => x.Entity).ToArray();

            foreach (var item in Modifiedobject)
            {
                if (item.GetType().Name == nameof(EntityModels.Entities.Posts.Post))
                {
                    var SourceObject = item as Post;
                    SourceObject.Slug = SourceObject.Title.Trim().Replace(" ", "-");

                }
                else if (item.GetType().Name == nameof(EntityModels.Entities.Products.Product))
                {
                    var SourceObject = item as Product;
                    SourceObject.Slug = SourceObject.Name.Trim().Replace(" ", "-");
                }
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
           // optionsBuilder.UseSqlServer("Server = .; Database = MyShopTestDb; Trusted_Connection = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Assembly EntityModelAssembly = Assembly.Load("EntityModels");
            modelBuilder.ApplyConfigurationsFromAssembly(EntityModelAssembly);
        }

    }
}
