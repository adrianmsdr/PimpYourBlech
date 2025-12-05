using Microsoft.EntityFrameworkCore;
using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Persistence.EFDatabase;

public sealed class ConfiguratorContext : DbContext
{
    public ConfiguratorContext() { }

    public ConfiguratorContext(DbContextOptions<ConfiguratorContext> options)
        : base(options)
    {
    }
    
    // definition der Datenbank Tabellen
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Configuration> Configurations { get; set; }
    public DbSet<EngineDetail> Engines { get; set; }
    public DbSet<RimDetail> Rims { get; set; }
    public DbSet<LightsDetail> Lights { get; set; }
    public DbSet<Order> Orders { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Connection String zur Datenbank
      // var cs ="Host=localhost;Port=5432;Database=configuratordb;Username=postgres;Password=postgres";
         var cs = "Host=100.126.42.89;Port=5432;Database=configuratordb;Username=postgres;Password=postgres";

        optionsBuilder.UseNpgsql(cs);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
  // CustomerID oder so als Primary key
        modelBuilder.Entity<Customer>()
            .HasKey(b => b.Id);
          
          
          modelBuilder.Entity<Product>()
              .HasOne(p => p.EngineDetail)
              .WithOne(d => d.Product)
              .HasForeignKey<EngineDetail>(d => d.ProductId);

          modelBuilder.Entity<Product>()
              .HasOne(p => p.RimDetail)
              .WithOne(d => d.Product)
              .HasForeignKey<RimDetail>(d => d.ProductId);

          modelBuilder.Entity<Product>()
              .HasOne(p => p.LightsDetail)
              .WithOne(d => d.Product)
              .HasForeignKey<LightsDetail>(d => d.ProductId);
      
//Später vielleicht auch eine eigne id ider wir machen nen datentyp Artikelnumnmer
        modelBuilder.Entity<Car>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Configuration>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<EngineDetail>();
    }


}