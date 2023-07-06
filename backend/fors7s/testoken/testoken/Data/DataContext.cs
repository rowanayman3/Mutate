using Microsoft.EntityFrameworkCore;
using System.Data;
using testoken.Models;

namespace testoken.Data
{
    public class DataContext :DbContext 
    {
        public DataContext(DbContextOptions<DataContext>options):base (options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Server=YASO01337;Database=dbforpro;Trusted_Connection=true;");
        //}

        //DataTable table = new DataTable();
        //string sqldatasource = 



        public DbSet<User> Users => Set<User> ();
    }

}
