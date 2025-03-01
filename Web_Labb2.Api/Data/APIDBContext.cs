using Microsoft.EntityFrameworkCore;

public class APIDBContext : DbContext
{
    public APIDBContext(DbContextOptions<APIDBContext> options) : base(options)
    {
    }

    public DbSet<AddressEntity> AdressEntitys { get; set; }
    public DbSet<CustomerEntity> CustomerEntitys { get; set; }
    public DbSet<ProductEntity> ProductEntitys { get; set; }

}