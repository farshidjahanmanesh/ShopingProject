using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityModels.Entities.Products
{
    

    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
       
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

    public class ProductImageConfig : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImageUrl).IsRequired();

        }
    }

}
