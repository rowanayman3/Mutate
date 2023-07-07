using Microsoft.EntityFrameworkCore;

namespace SuperMarket.Models
{
    public class SMDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }

        public SMDbContext(DbContextOptions<SMDbContext> options) : base(options) 
        {

        }
    }
}
