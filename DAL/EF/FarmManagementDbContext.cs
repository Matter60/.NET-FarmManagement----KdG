using System.Diagnostics;
using FarmManagement.BL.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FarmManagement.DAL.EF;

public class FarmManagementDbContext : DbContext
{
    
    public DbSet<Farm> Farms { get; set; }
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Harvest> Harvests { get; set; }

    public DbSet<FarmAnimal> FarmAnimals { get; set; }
    
    
    public FarmManagementDbContext(DbContextOptions options)
        :base(options)
    {
       
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.LogTo(msg => Debug.WriteLine(msg), LogLevel.Information);
    }

    public bool CreateDatabase(bool deleteDb)
    {
        if (deleteDb)
        {
            Database.EnsureDeleted();
        }
        return Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
       

        modelBuilder.Entity<FarmAnimal>()
            .HasOne(fa => fa.Farm)
            .WithMany(f => f.FarmAnimals)
            .HasForeignKey("FkFarmId")
            .IsRequired();
        
        modelBuilder.Entity<FarmAnimal>()
            .HasOne(fa => fa.Animal)
            .WithMany(a => a.FarmAnimals)
            .HasForeignKey("FkAnimalId")
            .IsRequired();
        
        modelBuilder.Entity<Harvest>()
            .HasOne(f => f.Farm)
            .WithMany(h => h.Harvests)
            .HasForeignKey("FkFarmId")
            .IsRequired();
        
        modelBuilder.Entity<FarmAnimal>().HasKey("FkFarmId","FkAnimalId");


    }
}