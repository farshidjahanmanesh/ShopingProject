using EntityModels.Entities.Posts;
using EntityModels.Entities.Products;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace EntityModels.Generators
{
    public class SlugGenerator : ValueGenerator
    {
        public override bool GeneratesTemporaryValues => false;
        
        protected override object NextValue([NotNullAttribute] EntityEntry entry)
        {
            switch (entry.Entity)
            {
                case Post post:
                    {
                       return post.Title.Trim().Replace(" ", "-");
                    }
                  //  break;
                case Product product:
                    {
                     //   product.Slug = product.Name.Trim().Replace(" ", "-");
                    }
                    break;
            }
            return "";
        }
    }
}
