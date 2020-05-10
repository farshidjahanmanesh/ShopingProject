using EntityModels.Generators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Entities.Posts
{
    public class Post:BasicEntity.IBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summery { get; set; }
        public string FullText { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsActive { get; set; }
        public string Slug { get; set; }
        public int AuthorId { get; set; }
        public string BaseImage { get; set; }
        public List<PostImage> Images { get; set; } = new List<PostImage>();
        public List<PostKeyword> Keywords { get; set; } = new List<PostKeyword>();
        public List<PostComment> Comments { get; set; } = new List<PostComment>();
    }

    public class PostConfig : IEntityTypeConfiguration<Post>
    {

        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Summery).HasMaxLength(700).IsRequired();
            builder.Property(x => x.FullText).IsRequired();
            builder.HasMany(x => x.Images).WithOne(x => x.Post).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Comments).WithOne(x => x.Post).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Keywords).WithOne(x => x.Post).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.Slug).HasMaxLength(400).HasValueGenerator<SlugGenerator>().IsRequired();
            builder.Property(x => x.BaseImage).IsRequired().HasMaxLength(450);
            builder.HasQueryFilter(x => x.IsActive == true);
            builder.HasIndex(x => x.Title);
        }
    }
}
