using EntityModels.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Entities.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsActive { get; set; }
        public int SellerId { get; set; }

        public Nullable<int> GroupId { get; set; }
        public ProductGroups Group { get; set; }


        public List<ProductDetail> Details { get; set; } = new List<ProductDetail>();
        public List<ProductImage> Images { get; set; } = new List<ProductImage>();
        public List<ProductComment> Comments { get; set; } = new List<ProductComment>();
        public List<ProductKeyword> Keywords { get; set; } = new List<ProductKeyword>();
        public List<LinkTogether> Links { get; set; } = new List<LinkTogether>();

    }

    public class ProductConfig: IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(300).IsRequired();
            builder.HasMany(x => x.Details).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Images).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Comments).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Keywords).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Links).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.Price);
            builder.HasIndex(x => x.PublishDate);
        }
    }

}
