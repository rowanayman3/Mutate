namespace ecommerce_template.Models
{
    public class Cart
    {
        public int Cartid { get; set; }
        public int Userid { get; set;}
        public int Productid { get; set; }
        public int price { get; set; }
        public int Quantity { get; set; }
        public string status { get; set; }
        public decimal discount { get; set; }
        public decimal TotalPrice { get; set; }

        
    }
}
