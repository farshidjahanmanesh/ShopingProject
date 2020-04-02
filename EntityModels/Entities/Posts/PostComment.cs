using EntityModels.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EntityModels.Entities.Posts
{
    public class PostComment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsActive { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }

    public class PostCommentConfig : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(400).IsRequired();
            builder.Property(x => x.Text).HasMaxLength(600).IsRequired();

        }
    }

}
