using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PimpYourBlech_Data.Models;


namespace PimpYourBlech_Data.Persistence.EFDatabase;

public sealed class ConfiguratorContext : DbContext, IDatabase
{
    public ConfiguratorContext() { }

    public ConfiguratorContext(DbContextOptions<ConfiguratorContext> options)
        : base(options)
    {
    }
    
    // definition der Datenbank Tabellen
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public int SaveChanges() => base.SaveChanges();
    public async Task<int> SaveChangesAsync()  => await base.SaveChangesAsync();

    public DbSet<Car> Cars { get; set; }
    public DbSet<Configuration> Configurations { get; set; }
    public DbSet<EngineDetail> Engines { get; set; }
    public DbSet<RimDetail> Rims { get; set; }
    public DbSet<LightsDetail> Lights { get; set; }
    
    public DbSet<ColorDetail> Colors { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderPosition> OrderPositions { get; set; }

    public DbSet<CommunityQuestion> CommunityQuestions { get; set; }
    public DbSet<CommunityAnswer> CommunityAnswers { get; set; }
    
    public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
    
    public DbSet<PaymentValue> PaymentValues { get; set; }
    
    
    public async Task<int> GetNextArticleNumberAsync()
    {
        long next = await Database
            .SqlQuery<long>($"SELECT nextval('\"ArticleNumberSeq\"') AS \"Value\"")
            .SingleAsync();

        return checked((int)next);
    }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Connection String zur Datenbank
       var cs ="Host=localhost;Port=5432;Database=configuratordb;Username=postgres;Password=postgres";
        // var cs = "Host=100.126.42.89;Port=5432;Database=configuratordb;Username=postgres;Password=postgres";

        optionsBuilder.UseNpgsql(cs);
    }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasSequence<int>("ArticleNumberSeq").StartsAt(100000).IncrementsBy(1);
        modelBuilder.Entity<Product>().HasIndex(p => p.ArticleNumber).IsUnique();
        modelBuilder.Entity<Product>().Property(p => p.ArticleNumber).HasMaxLength(7);

        // Product -> Car (1:n)  (EMPFOHLEN: Car.Products in Car.cs anlegen)
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Car)
            .WithMany(c => c.Products)      // statt .WithMany()
            .HasForeignKey(p => p.CarId)
            .OnDelete(DeleteBehavior.Restrict);

        // Product -> Details (1:1)
        modelBuilder.Entity<Product>().HasOne(p => p.EngineDetail).WithOne(d => d.Product)
            .HasForeignKey<EngineDetail>(d => d.ProductId);

        modelBuilder.Entity<Product>().HasOne(p => p.RimDetail).WithOne(d => d.Product)
            .HasForeignKey<RimDetail>(d => d.ProductId);

        modelBuilder.Entity<Product>().HasOne(p => p.LightsDetail).WithOne(d => d.Product)
            .HasForeignKey<LightsDetail>(d => d.ProductId);

        modelBuilder.Entity<Product>().HasOne(p => p.ColorDetail).WithOne(d => d.Product)
            .HasForeignKey<ColorDetail>(d => d.ProductId);

        
            
        // Configuration -> Customer (1:n)
        modelBuilder.Entity<Configuration>()
            .HasOne(cfg => cfg.Customer)
            .WithMany(c => c.Configurations)
            .HasForeignKey(cfg => cfg.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuration -> Car (n:1)
        modelBuilder.Entity<Configuration>()
            .HasOne(cfg => cfg.Car)
            .WithMany() // optional: Car.Configurations, wenn du willst
            .HasForeignKey(cfg => cfg.CarId);

        // Community
        modelBuilder.Entity<CommunityQuestion>(q =>
        {
            q.Property(x => x.Content).IsRequired();
            q.Property(x => x.CreatedAt).HasDefaultValueSql("NOW()");
            q.HasMany(x => x.Answers)
             .WithOne(a => a.Question)
             .HasForeignKey(a => a.QuestionId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CommunityAnswer>(a =>
        {
            a.Property(x => x.Content).IsRequired();
            a.Property(x => x.CreatedAt).HasDefaultValueSql("NOW()");
        });
        
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.DeliveryAddresses)
            .WithOne(d => d.Customer)
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.PaymentValues)
            .WithOne(d => d.Customer)
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderPosition>()
            .HasOne(i => i.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.DeliveryAddress)
            .WithMany()
            .HasForeignKey(o => o.DeliveryAddressId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Order>()
            .HasOne(o => o.PaymentValue)
            .WithMany()
            .HasForeignKey(o => o.PaymentValueId)
            .OnDelete(DeleteBehavior.Restrict);
        
        
        
    }

    int IDatabase.SaveChanges() => base.SaveChanges();
}