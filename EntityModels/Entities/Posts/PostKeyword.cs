using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityModels.Entities.Posts
{
    public class PostKeyword
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }

    public class PostKeywordConfig : IEntityTypeConfiguration<PostKeyword>
    {
        public void Configure(EntityTypeBuilder<PostKeyword> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Text).HasMaxLength(350).IsRequired();

        }
    }
}
