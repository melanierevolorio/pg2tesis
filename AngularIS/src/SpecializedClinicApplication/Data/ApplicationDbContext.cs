using Microsoft.EntityFrameworkCore;
using SpecializedClinicApplication.Data.Models.Inventory;

namespace SpecializedClinicApplication.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Product> Products { get; set; }
}