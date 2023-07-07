namespace ecommerce_template.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public List<Users> ListofUsers { get; set; }
        public Users user { get; set; }
        public List<Products> ListofProducts { get; set; }
        public Products Product { get; set; }
        public List<Categories> Listofcategory { get; set; }
        public Categories Category { get; set; }
        public List<Cart> ListofCarts { get; set; }
        public List<Orders> ListofOrders { get; set; }
        public Orders Order { get; set; }
        public List<OrderItems> ListofOrderitems { get; set; }
        public OrderItems OrderItem { get; set; }
    }
}
