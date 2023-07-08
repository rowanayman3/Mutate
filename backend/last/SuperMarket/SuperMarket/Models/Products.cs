namespace SuperMarket.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public decimal discount { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
