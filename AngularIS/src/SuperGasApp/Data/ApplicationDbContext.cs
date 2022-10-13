using Microsoft.EntityFrameworkCore;
using SuperGasApp.Data.Models.Inventory;

namespace SuperGasApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Product> Products { get; set; }
}