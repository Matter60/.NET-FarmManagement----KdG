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
}