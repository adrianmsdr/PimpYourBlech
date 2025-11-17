using Microsoft.EntityFrameworkCore;

namespace TestAutoKonfigurator.Persistence.EFDatabase;

public sealed class ConfiguratorContext : DbContext
{
    // definition der Datenbank Tabellen
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Configuration> Configurations { get; set; }
    public DbSet<Engine> Engines { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Connection String zur Datenbank
        var cs ="Host=localhost;Port=5432;Database=configuratordb;Username=postgres;Password=postgres";
        optionsBuilder.UseNpgsql(cs);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
  // CustomerID oder so als Primary key
        modelBuilder.Entity<Customer>()
            .HasKey(b => b.Id);
          
          // Prudukte mit Artikelnummern als pk?
          modelBuilder.Entity<Product>()
              .HasKey(a => a.ArticleNumber);
      
//Später vielleicht auch eine eigne id ider wir machen nen datentyp Artikelnumnmer
        modelBuilder.Entity<Car>()
            .HasKey(a => a.Name);

        modelBuilder.Entity<Configuration>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Engine>();
    }


}