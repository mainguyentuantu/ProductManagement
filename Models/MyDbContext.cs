using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-00I7L7J\\TU;Initial Catalog=DBProductManagement;Persist Security Info=True;User ID=sa;Password=12345;Encrypt=True;Trust Server Certificate=True");
            }
        }


    }
}
