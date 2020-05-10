using EntityModels.Entities.Categories;
using EntityModels.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityModels.DTOs.CategoriesDtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public bool IsActive { get; private set; }

        public List<ProductGroups> Groups { get; set; } = new List<ProductGroups>();
    }
}
