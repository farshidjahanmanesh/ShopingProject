using EntityModels.Entities.BasicEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EntityModels.Entities.Products
{
    public class ProductComment:IBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsActive { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

    public class ProductCommentConfig : IEntityTypeConfiguration<ProductComment>
    {
        public void Configure(EntityTypeBuilder<ProductComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(400).IsRequired();
            builder.HasQueryFilter(x => x.IsActive == true);
            builder.Property(x => x.Text).HasMaxLength(600).IsRequired();

        }
    }

}
