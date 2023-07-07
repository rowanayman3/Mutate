namespace ecommerce_template.Models
{
    public class OrderItems
    {
        public int OrderItemsID { get; set; }
        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public int Price { get; set; }
        public decimal discount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
