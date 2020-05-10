﻿using System.ComponentModel.DataAnnotations;

namespace EntityModels.DTOs.ProductDtos
{
    public class FeaturedProductDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string BaseImage { get; set; }
        [Required]
        public double Price { get; set; }
        public string Slug { get; private set; }
        public string TagName { get; set; }
    }
}
