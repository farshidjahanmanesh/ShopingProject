using EntityModels.DTOs.CategoriesDtos;
using EntityModels.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityModels.DTOs.ProductDtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string BaseImage { get; set; }
        [Required]
        public double Price { get; set; }
        public string Slug { get;private set; }
        public int Count { get; set; }
        public DateTime PublishDate { get;private set; }
        public bool IsActive { get;private set; }

        public int SellerId { get; set; }

        public int GroupId { get; set; }
        public ProductGroupsDto Group { get; set; }
        public string Summery { get; set; }


        public List<ProductDetail> Details { get; set; } = new List<ProductDetail>();
        public List<ProductImage> Images { get; set; } = new List<ProductImage>();
        public List<ProductCommentDto> Comments { get; set; } = new List<ProductCommentDto>();
        public List<ProductKeyword> Keywords { get; set; } = new List<ProductKeyword>();
    }
}
