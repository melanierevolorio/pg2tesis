using Microsoft.EntityFrameworkCore;
using SuperGasApp.Data.Models.Inventory;

namespace SuperGasApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customers> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<OrdersProducts> OrdersProducts { get; set; }
}