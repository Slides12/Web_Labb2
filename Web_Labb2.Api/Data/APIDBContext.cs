using Microsoft.EntityFrameworkCore;
using Web_Labb2.Shared.Models;

public class APIDBContext : DbContext
{
    public APIDBContext(DbContextOptions<APIDBContext> options) : base(options)
    {
    }

    public DbSet<AddressEntity> AdressEntitys { get; set; }
    public DbSet<CustomerEntity> CustomerEntitys { get; set; }
    public DbSet<ProductEntity> ProductEntitys { get; set; }
    public DbSet<User> UserEntitys { get; set; }
    public DbSet<OrderInfo> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderDetail>()
            .HasKey(od => new { od.OrderID, od.OrderDetailID });
    }

}