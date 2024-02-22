using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Models
{
    public class MyDbContext : DbContext
    { 
        
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.MaSP);
        }

    }
}
