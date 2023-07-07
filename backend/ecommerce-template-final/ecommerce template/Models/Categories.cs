namespace ecommerce_template.Models
{
    public class Categories
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductID { get; set; }
        public string Type { get; set; }
        public string ImageURL { get; set; }
    }
}
