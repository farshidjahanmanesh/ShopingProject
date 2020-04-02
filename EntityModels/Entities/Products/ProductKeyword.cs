using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityModels.Entities.Products
{
    public class ProductKeyword
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

    public class ProductKeywordConfig : IEntityTypeConfiguration<ProductKeyword>
    {
        public void Configure(EntityTypeBuilder<ProductKeyword> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Text).HasMaxLength(350).IsRequired();

        }
    }
}
