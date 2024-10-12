using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
    }

    public DbSet<BpkbModel> Bpkbs { get; set; }
    public DbSet<StorageLocationModel> StorageLocations { get; set; }
    public DbSet<UserModel> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define any constraints or relationships
        modelBuilder.Entity<BpkbModel>()
            .HasOne(b => b.StorageLocation)
            .WithMany()
            .HasForeignKey(b => b.LocationId);
    }
}
