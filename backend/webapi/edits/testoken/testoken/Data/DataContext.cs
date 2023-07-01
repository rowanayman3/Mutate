using Microsoft.EntityFrameworkCore;
using testoken.Models;

namespace testoken.Data
{
    public class DataContext :DbContext 
    {
        public DataContext(DbContextOptions<DataContext>options):base (options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=YASO01337;Database=dbforpro;Trusted_Connection=true;");
        }


        public DbSet<User> Users => Set<User> ();
    }

}
