using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Entities.Site
{
    public class NewsLetter
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }

    public class NewsletterConfig : IEntityTypeConfiguration<NewsLetter>
    {
        public void Configure(EntityTypeBuilder<NewsLetter> builder)
        {
            builder.Property(x => x.Email).HasMaxLength(350).IsRequired();
        }
    }
}
