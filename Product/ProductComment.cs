using System;

namespace EntityModels.Entities.Product
{
    public class ProductComment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsActive { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }


}
