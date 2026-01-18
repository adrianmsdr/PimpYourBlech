using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PimpYourBlech_Contracts.Enums;
using PimpYourBlech_Data.Models;


namespace PimpYourBlech_Data.Persistence.EFDatabase;

public sealed class ConfiguratorContext : DbContext, IDatabase
{
    public ConfiguratorContext()
    {
    }

    public ConfiguratorContext(DbContextOptions<ConfiguratorContext> options)
        : base(options)
    {
    }

    // definition der Datenbank Tabellen
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public int SaveChanges() => base.SaveChanges();
    public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();

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
        var cs = "Host=localhost;Port=5432;Database=configuratordb;Username=postgres;Password=postgres";
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
            .WithMany(c => c.Products) // statt .WithMany()
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

        //AB HIER SEEDING 

/*=====================================================
  CUSTOMER
=====================================================*/
        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                Id = 1,
                FirstName = "Max",
                LastName = "Mustermann",
                Username = "MusterAdmin",
                PasswordHash = "P6zbHsZ98YHkhf6yoM/EMMjAOt31qqUEdCRYJrKpKqs=",
                Telefon = "0123456789",
                MailAddress = "mustermail-admin.adresse@mustermail.de",
                AdminRights = true,
                ImagePath = "/CustomerImages/Car1.png"
            }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                Id = 2,
                FirstName = "Max",
                LastName = "Mustermann",
                Username = "MusterMax",
                PasswordHash = "P6zbHsZ98YHkhf6yoM/EMMjAOt31qqUEdCRYJrKpKqs=",
                Telefon = "0123456789",
                MailAddress = "mustermail.adresse@mustermail.de",
                AdminRights = false,
                ImagePath = "/CustomerImages/Car1.png"
            }
        );

/*=====================================================
  CARS
=====================================================*/
        modelBuilder.Entity<Car>().HasData(
            new Car
            {
                Id = 1,
                Name = "Golf",
                DateProduction = "2025",
                DatePermit = "2026",
                Brand = "Volkswagen",
                Model = "GTI 2025",
                Price = 45000,
                PS = 325,
                Quantity = 20
            },
            new Car
            {
                Id = 2,
                Name = "Polo",
                DateProduction = "2020",
                DatePermit = "2021",
                Brand = "Volkswagen",
                Model = "Polo 2020",
                Price = 25000,
                PS = 225,
                Quantity = 15
            }
        );

/*=====================================================
  PRODUCTS
=====================================================*/
        modelBuilder.Entity<Product>().HasData(

            // ================= GOLF =================

            // Felgen
            new Product
            {
                ProductId = 1, CarId = 1, Name = "Queenstown Felge", ArticleNumber = "100000", Brand = "Volkswagen",
                Description = "Sportliche Alufelge mit roten Akzenten.", Quantity = 10, Price = 1000,
                ProductType = ProductType.Felge
            },
            new Product
            {
                ProductId = 2, CarId = 1, Name = "Warmenau Performance Felge", ArticleNumber = "100001",
                Brand = "Volkswagen", Description = "Aerodynamische Premium-Felge für sportliche Fahrweise.",
                Quantity = 10, Price = 1200, ProductType = ProductType.Felge
            },
            new Product
            {
                ProductId = 21, CarId = 1, Name = "Pretoria Sport Felge", ArticleNumber = "100011",
                Brand = "Volkswagen", Description = "Leichte Schmiedefelge mit hoher Stabilität.", Quantity = 8,
                Price = 1350, ProductType = ProductType.Felge
            },

            // Motoren
            new Product
            {
                ProductId = 3, CarId = 1, Name = "GTI 2.0 TFSI Performance Motor", ArticleNumber = "100002",
                Brand = "Volkswagen", Description = "325 PS Turbo-Benzinmotor.", Quantity = 5, Price = 2500,
                ProductType = ProductType.Motor
            },
            new Product
            {
                ProductId = 4, CarId = 1, Name = "GTI Clubsport RS Motor", ArticleNumber = "100003",
                Brand = "Volkswagen", Description = "Hochleistungsmotor mit Rennsportabstimmung.", Quantity = 3,
                Price = 3400, ProductType = ProductType.Motor
            },
            new Product
            {
                ProductId = 15, CarId = 1, Name = "GTI EcoBoost Hybrid Motor", ArticleNumber = "100008",
                Brand = "Volkswagen", Description = "Effizienter Hybridmotor mit ruhigem Lauf.", Quantity = 4,
                Price = 3900, ProductType = ProductType.Motor
            },

            // Lichter
            new Product
            {
                ProductId = 5, CarId = 1, Name = "IQ.Light LED Matrix Pro", ArticleNumber = "100004",
                Brand = "Volkswagen", Description = "Adaptive Matrix-LED-Scheinwerfer.", Quantity = 10, Price = 1450,
                ProductType = ProductType.Lichter
            },
            new Product
            {
                ProductId = 6, CarId = 1, Name = "Dynamic Vision LED Blackline", ArticleNumber = "100007",
                Brand = "Volkswagen", Description = "Sportlicher LED-Scheinwerfer mit dunklem Gehäuse.", Quantity = 8,
                Price = 1580, ProductType = ProductType.Lichter
            },
            new Product
            {
                ProductId = 16, CarId = 1, Name = "NightDrive LED UltraBeam", ArticleNumber = "100009",
                Brand = "Volkswagen", Description = "Extrem helle LED-Scheinwerfer für maximale Sicht.", Quantity = 6,
                Price = 1750, ProductType = ProductType.Lichter
            },

            // Farben
            new Product
            {
                ProductId = 7, CarId = 1, Name = "Tornadorot", ArticleNumber = "100005", Brand = "Volkswagen",
                Description = "Kräftige Sportlackierung.", Quantity = 20, Price = 1800, ProductType = ProductType.Lack
            },
            new Product
            {
                ProductId = 8, CarId = 1, Name = "Metallic Weiß Perleffekt", ArticleNumber = "100006",
                Brand = "Volkswagen", Description = "Eleganter Perlglanz.", Quantity = 20, Price = 1900,
                ProductType = ProductType.Lack
            },
            new Product
            {
                ProductId = 17, CarId = 1, Name = "Deep Black Pearl", ArticleNumber = "100010", Brand = "Volkswagen",
                Description = "Tiefschwarze Premium-Lackierung.", Quantity = 15, Price = 2100,
                ProductType = ProductType.Lack
            },

            // ================= POLO =================

            // Felgen
            new Product
            {
                ProductId = 22, CarId = 2, Name = "Astana Felge", ArticleNumber = "200010", Brand = "Volkswagen",
                Description = "Klassische Alufelge für Alltag und Komfort.", Quantity = 12, Price = 850,
                ProductType = ProductType.Felge
            },
            new Product
            {
                ProductId = 23, CarId = 2, Name = "Bergamo Sport Felge", ArticleNumber = "200011", Brand = "Volkswagen",
                Description = "Sportliche Mehrspeichenfelge.", Quantity = 10, Price = 980,
                ProductType = ProductType.Felge
            },
            new Product
            {
                ProductId = 24, CarId = 2, Name = "Verona Black Performance Felge", ArticleNumber = "200012",
                Brand = "Volkswagen", Description = "Schwarze Performance-Felge.", Quantity = 6, Price = 1100,
                ProductType = ProductType.Felge
            },

            // Motoren
            new Product
            {
                ProductId = 11, CarId = 2, Name = "Polo 1.0 TSI BlueMotion Motor", ArticleNumber = "200003",
                Brand = "Volkswagen", Description = "Effizienter Stadtturbomotor.", Quantity = 8, Price = 1800,
                ProductType = ProductType.Motor
            },
            new Product
            {
                ProductId = 12, CarId = 2, Name = "Polo 1.5 TSI GT-Line Motor", ArticleNumber = "200004",
                Brand = "Volkswagen", Description = "Sportlicher Turbomotor.", Quantity = 6, Price = 2400,
                ProductType = ProductType.Motor
            },
            new Product
            {
                ProductId = 19, CarId = 2, Name = "Polo 1.2 Hybrid Drive", ArticleNumber = "200008",
                Brand = "Volkswagen", Description = "Hybridantrieb mit niedrigem Verbrauch.", Quantity = 5,
                Price = 2600, ProductType = ProductType.Motor
            },

            // Lichter
            new Product
            {
                ProductId = 13, CarId = 2, Name = "Polo LED Comfort Beam", ArticleNumber = "200005",
                Brand = "Volkswagen", Description = "Gleichmäßige LED-Ausleuchtung.", Quantity = 10, Price = 980,
                ProductType = ProductType.Lichter
            },
            new Product
            {
                ProductId = 14, CarId = 2, Name = "Polo LED NightVision Plus", ArticleNumber = "200006",
                Brand = "Volkswagen", Description = "Erweiterte Reichweite bei Nacht.", Quantity = 8, Price = 1150,
                ProductType = ProductType.Lichter
            },
            new Product
            {
                ProductId = 20, CarId = 2, Name = "Polo Adaptive Pixel Light", ArticleNumber = "200009",
                Brand = "Volkswagen", Description = "Blendfreies Fernlicht mit Pixel-Technologie.", Quantity = 6,
                Price = 1350, ProductType = ProductType.Lichter
            },

            // Farben
            new Product
            {
                ProductId = 9, CarId = 2, Name = "Crystal Ice Blue Metallic", ArticleNumber = "200001",
                Brand = "Volkswagen", Description = "Kühle Metallic-Lackierung.", Quantity = 20, Price = 1600,
                ProductType = ProductType.Lack
            },
            new Product
            {
                ProductId = 10, CarId = 2, Name = "Kings Red Velvet", ArticleNumber = "200002", Brand = "Volkswagen",
                Description = "Edler Rotton mit Tiefenglanz.", Quantity = 20, Price = 1700,
                ProductType = ProductType.Lack
            },
            new Product
            {
                ProductId = 18, CarId = 2, Name = "Urban Grey Metallic", ArticleNumber = "200007", Brand = "Volkswagen",
                Description = "Moderne urbane Graulackierung.", Quantity = 18, Price = 1650,
                ProductType = ProductType.Lack
            }
        );

/*=====================================================
  DETAILS
=====================================================*/

        modelBuilder.Entity<RimDetail>().HasData(
            new RimDetail { Id = 1, ProductId = 1, DiameterInInch = 19, WidthInInch = 8 },
            new RimDetail { Id = 2, ProductId = 2, DiameterInInch = 20, WidthInInch = 8.5m },
            new RimDetail { Id = 3, ProductId = 21, DiameterInInch = 18, WidthInInch = 7.5m },
            new RimDetail { Id = 4, ProductId = 22, DiameterInInch = 16, WidthInInch = 6.5m },
            new RimDetail { Id = 5, ProductId = 23, DiameterInInch = 17, WidthInInch = 7.0m },
            new RimDetail { Id = 6, ProductId = 24, DiameterInInch = 18, WidthInInch = 7.5m }
        );

        modelBuilder.Entity<EngineDetail>().HasData(
            new EngineDetail
            {
                Id = 1, ProductId = 3, Fuel = Fuel.Benzin, Ps = 325, Kw = 239, Displacement = "2.0",
                Gear = Gear.Automatik6Gang
            },
            new EngineDetail
            {
                Id = 2, ProductId = 4, Fuel = Fuel.Benzin, Ps = 360, Kw = 265, Displacement = "2.0",
                Gear = Gear.Automatik6Gang
            },
            new EngineDetail
            {
                Id = 3, ProductId = 15, Fuel = Fuel.Hybrid, Ps = 280, Kw = 210, Displacement = "1.8",
                Gear = Gear.Automatik6Gang
            },
            new EngineDetail
            {
                Id = 4, ProductId = 11, Fuel = Fuel.Benzin, Ps = 110, Kw = 81, Displacement = "1.0",
                Gear = Gear.Manuell6Gang
            },
            new EngineDetail
            {
                Id = 5, ProductId = 12, Fuel = Fuel.Benzin, Ps = 150, Kw = 110, Displacement = "1.5",
                Gear = Gear.Automatik6Gang
            },
            new EngineDetail
            {
                Id = 6, ProductId = 19, Fuel = Fuel.Hybrid, Ps = 130, Kw = 96, Displacement = "1.2",
                Gear = Gear.Automatik6Gang
            }
        );

        modelBuilder.Entity<ColorDetail>().HasData(
            new ColorDetail { Id = 1, ProductId = 7, DisplayName = "Tornadorot" },
            new ColorDetail { Id = 2, ProductId = 8, DisplayName = "Metallic Weiß Perleffekt" },
            new ColorDetail { Id = 3, ProductId = 17, DisplayName = "Deep Black Pearl" },
            new ColorDetail { Id = 4, ProductId = 9, DisplayName = "Crystal Ice Blue Metallic" },
            new ColorDetail { Id = 5, ProductId = 10, DisplayName = "Kings Red Velvet" },
            new ColorDetail { Id = 6, ProductId = 18, DisplayName = "Urban Grey Metallic" }
        );

        modelBuilder.Entity<LightsDetail>().HasData(
            new LightsDetail { Id = 1, ProductId = 5, Lumen = 3200, IsLed = true },
            new LightsDetail { Id = 2, ProductId = 6, Lumen = 3000, IsLed = true },
            new LightsDetail { Id = 3, ProductId = 16, Lumen = 3800, IsLed = true },
            new LightsDetail { Id = 4, ProductId = 13, Lumen = 2600, IsLed = true },
            new LightsDetail { Id = 5, ProductId = 14, Lumen = 3100, IsLed = true },
            new LightsDetail { Id = 6, ProductId = 20, Lumen = 3400, IsLed = true }
        );

/*=====================================================
  DELIVERY ADDRESS
=====================================================*/
        modelBuilder.Entity<DeliveryAddress>().HasData(
            new DeliveryAddress
            {
                Id = 1,
                CustomerId = 1,
                Salutation = "Herr",
                Surname = "Admin",
                Lastname = "Mustermann",
                Street = "Musterstraße",
                HouseNumber = "12A",
                Town = "Rosenheim",
                PostalCode = "83022",
                Country = "DE"
            }
        );

        modelBuilder.Entity<DeliveryAddress>().HasData(
            new DeliveryAddress
            {
                Id = 2,
                CustomerId = 2,
                Salutation = "Herr",
                Surname = "Max",
                Lastname = "Mustermann",
                Street = "Musterstraße",
                HouseNumber = "12A",
                Town = "Rosenheim",
                PostalCode = "83022",
                Country = "DE"
            }
        );

/*=====================================================
  PAYMENT VALUE
=====================================================*/
        modelBuilder.Entity<PaymentValue>().HasData(
            new PaymentValue
            {
                Id = 1,
                CustomerId = 1,
                AccountOwner = "Max Mustermann",
                Iban = "DE44500105175407324931",
                Bic = "INGDDEFFXXX"
            }
        );

/*=====================================================
  ORDER
=====================================================*/
        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                OrderId = 1,
                CustomerId = 1,
                DeliveryAddressId = 1,
                PaymentValueId = 1,
                OrderDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                TotalPrice = 2000m
            }
        );

/*=====================================================
  ORDER POSITION
=====================================================*/
        modelBuilder.Entity<OrderPosition>().HasData(
            new OrderPosition
            {
                OrderPositionId = 1,
                OrderId = 1,
                ArticleNumber = "100000",
                Name = "Queenstown Felge",
                Brand = "Volkswagen",
                Type = ProductType.Felge,
                Quantity = 2,
                UnitPrice = 1000m
            }
        );

        /*=====================================================
    CONFIGURATIONS
  =====================================================*/
        modelBuilder.Entity<Configuration>().HasData(

            // ===== User 1 =====
            new Configuration
            {
                Id = 1,
                Name = "Max' Golf Konfiguration",
                CustomerId = 1,
                CarId = 1
            },

            new Configuration
            {
                Id = 2,
                Name = "Max' Polo Konfiguration",
                CustomerId = 1,
                CarId = 2
            },

            // ===== User 2 =====
            new Configuration
            {
                Id = 3,
                Name = "Mustermann Polo Setup",
                CustomerId = 2,
                CarId = 2
            },

            new Configuration
            {
                Id = 4,
                Name = "Mustermann Golf Setup",
                CustomerId = 2,
                CarId = 1
            }
        );


        // ================= EXTRAS =================
        modelBuilder.Entity<Product>().HasData(

            new Product
            {
                ProductId = 30,
                CarId = 1,
                Name = "GTI Carbon Heckspoiler",
                ArticleNumber = "300001",
                Brand = "Volkswagen",
                Description = "Sportlicher Carbon-Heckspoiler für verbesserte Aerodynamik.",
                Quantity = 5,
                Price = 950,
                ProductType = ProductType.Spoiler
            },

            new Product
            {
                ProductId = 31,
                CarId = 2,
                Name = "Polo Sport Heckspoiler",
                ArticleNumber = "300002",
                Brand = "Volkswagen",
                Description = "Kompakter Sportspoiler für den Polo.",
                Quantity = 7,
                Price = 720,
                ProductType = ProductType.Spoiler
            }
        );
    }

    int IDatabase.SaveChanges() => base.SaveChanges();
}