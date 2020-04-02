using System;
using System.Collections.Generic;
using System.Text;
using EntityModels.Entities.Products;
using EntityModels.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityModels.Entities
{
    public class LinkTogether
    {
        public int Id { get; set; }
        public string ProductTextLink { get; set; }
        public string PostTextLink { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }

    public class LinkTogetherConfig : IEntityTypeConfiguration<LinkTogether>
    {
        public void Configure(EntityTypeBuilder<LinkTogether> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProductTextLink).HasMaxLength(350).IsRequired();
            builder.Property(x => x.PostTextLink).HasMaxLength(350).IsRequired();

        }
    }


}
