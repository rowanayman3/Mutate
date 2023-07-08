
namespace ecommerce_template.Models
{
    public class Orders
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string OrderNo { get;  set; }
        public decimal OrderTotal { get; set; }
        public string OrderStatus { get; set; }
    }
}
