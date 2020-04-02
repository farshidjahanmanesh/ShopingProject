using EntityModels.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace EntityModels.Entities.Categories
{
    public class ProductGroups
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsActive { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }


        public List<Product> Products { get; set; } = new List<Product>();
    }

    public class ProductGroupsConfig : IEntityTypeConfiguration<ProductGroups>
    {
        public void Configure(EntityTypeBuilder<ProductGroups> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Text).HasMaxLength(300).IsRequired();
            builder.HasMany(x => x.Products).WithOne(x => x.Group).HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.SetNull);

        }
    }


}
