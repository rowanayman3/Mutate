namespace SuperMarket.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
