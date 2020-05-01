using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityModels.Entities.Posts
{
    public class PostImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
       
        public int PostId { get; set; }
        public Post Post { get; set; }
    }

    public class PostImageConfig : IEntityTypeConfiguration<PostImage>
    {
        public void Configure(EntityTypeBuilder<PostImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImageUrl).IsRequired();

        }
    }
}
