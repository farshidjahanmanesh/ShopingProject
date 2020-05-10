using System;
using System.ComponentModel.DataAnnotations;

namespace EntityModels.DTOs.ProductDtos
{
    public class ProductCommentDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime PublishDate { get;private set; }
        public bool IsActive { get;private set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
