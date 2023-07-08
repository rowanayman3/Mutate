namespace SuperMarket.Models
{
    public class Categories
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public List<Products> Products { get; set; }

    }
}
