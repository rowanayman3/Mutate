namespace ecommerce_template.Models
{
    public class Products
    {
        public int  ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string type { get; set; }
        public int Quantity { get; set; }
        public string imageUrl { get; set; }
        public decimal discount { get; set; }
        public string status { get; set; }
    }
}
