using MaretOrg2.Domain;
using Microsoft.EntityFrameworkCore;

namespace MaretOrg2.Infrastructure;

public class DatabaseManager :DbContext
{
    static readonly string connectionString = "Server=localhost; User ID=root; Password=root; Database=test";

    public DbSet<User> Users { get; set; }
    public DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        
        optionsBuilder.UseNpgsql(
            "Host=localhost;Database=ORGMarket;Username=postgres;Password=sahawndx119"
        );
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Items)
            .WithMany(i => i.Users)
            .UsingEntity(j => j.ToTable("UserItems"));
    }
    
    
}