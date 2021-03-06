﻿using EntityModels.DTOs.ProductDtos;

namespace UI.ViewModels
{
    public class SliderViewModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Summery { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
    }

}
