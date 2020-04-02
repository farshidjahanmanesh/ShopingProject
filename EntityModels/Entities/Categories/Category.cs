using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Entities.Categories
{
    public class Category
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsActive { get; set; }

        public List<ProductGroups> Groups { get; set; } = new List<ProductGroups>();
    }
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Text).HasMaxLength(300).IsRequired();
            builder.HasMany(x => x.Groups).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);

        }
    }








}
