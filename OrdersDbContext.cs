using Microsoft.EntityFrameworkCore;


public class OrdersDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
    {
    }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the Order entity
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
     //   optionsBuilder.UseSqlServer("Data Source=database-1.c41enn7adu8i.us-east-2.rds.amazonaws.com;Initial Catalog=Orders;User ID=admin;Password=12345678;Encrypt=false");
    }
}