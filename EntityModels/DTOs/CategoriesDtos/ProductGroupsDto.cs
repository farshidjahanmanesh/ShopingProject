using EntityModels.DTOs.ProductDtos;
using EntityModels.Entities.Categories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityModels.DTOs.CategoriesDtos
{
    public class ProductGroupsDto
    {
        public int Id { get; set; }
        
        public string Text { get; set; }
        public bool IsActive { get;private set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }


        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
