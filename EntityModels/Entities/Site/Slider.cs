using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Entities.Site
{
    public class Slider
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Summery { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }
    }

    public class SliderConfig : IEntityTypeConfiguration<Slider>
    {
       
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(x => x.Path).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Summery).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Tag).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(300).IsRequired();
        }
    }
}
