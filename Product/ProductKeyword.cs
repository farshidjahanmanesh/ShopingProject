namespace EntityModels.Entities.Product
{
    public class ProductKeyword
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }


}
