using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Entities.Product
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsActive { get; set; }
        public int SellerId { get; set; }


        public List<ProductDetail> Details { get; set; } = new List<ProductDetail>();
        public List<ProductImage> Images { get; set; } = new List<ProductImage>();
        public List<ProductComment> Comments { get; set; } = new List<ProductComment>();
        public List<ProductKeyword> Keywords { get; set; } = new List<ProductKeyword>();
    }


}
