using EntityModels.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.DTOs.SiteDtos
{
    public class SliderDto
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
