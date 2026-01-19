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
                DateProduction = "10/2025",
                DatePermit = "01/2026",
                Brand = "Volkswagen",
                Model = "GTI 2025",
                Price = 44999.99m,
                PS = 325,
                Quantity = 20
            },
            new Car
            {
                Id = 2,
                Name = "Polo",
                DateProduction = "11/2020",
                DatePermit = "01/2021",
                Brand = "Volkswagen",
                Model = "Polo 2020",
                Price = 24999.99m,
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
                Description = "Die Queenstown Felge steht für sportliches Design und solide Verarbeitung. Mit einem Durchmesser von 19 Zoll und einer Breite von 8 Zoll verleiht sie dem Fahrzeug eine kraftvolle, moderne Optik, ohne dabei übertrieben zu wirken. Das markante Mehrspeichen-Design sorgt für einen dynamischen Auftritt und lässt die Bremsanlage optisch größer und präsenter erscheinen.\n\nGefertigt für Volkswagen-Fahrzeuge, verbindet diese Felge Alltagstauglichkeit mit einem klaren Performance-Look. Sie eignet sich sowohl für den täglichen Einsatz als auch für Fahrer, die ihrem Fahrzeug eine deutlich aufgewertete Erscheinung verleihen wollen, ohne ins Extreme zu gehen.\n\nDank der ausgewogenen Dimensionen bietet die Queenstown Felge ein gutes Verhältnis aus Stabilität, Fahrkomfort und sportlicher Straßenlage. Eine ideale Wahl für alle, die Wert auf Qualität, saubere Optik und eine stimmige Gesamtwirkung legen.",
                Quantity = 10, Price = 999.99m,
                ProductType = ProductType.Felge
            },
            new Product
            {
                ProductId = 2, CarId = 1, Name = "Warmenau Performance Felge", ArticleNumber = "100001",
                Brand = "Volkswagen", Description = "Die Warmenau Performance Felge richtet sich klar an Fahrer, die keine Kompromisse wollen. Mit einem großzügigen Durchmesser von 20 Zoll und einer Breite von 8,5 Zoll setzt sie ein deutlich sportliches Statement und unterstreicht den Performance-Charakter des Fahrzeugs schon im Stand. Das präzise, kantige Speichendesign wirkt technisch, aggressiv und hochwertig zugleich.\n\nEntwickelt für Volkswagen-Fahrzeuge, verbindet diese Felge modernes Motorsport-Design mit hoher Alltagstauglichkeit. Die klare Linienführung sorgt für eine starke Tiefenwirkung und bringt besonders bei dunklen Fahrzeugfarben ihre volle Wirkung zur Geltung.\n\nDurch die breitere Auslegung bietet die Warmenau Performance Felge eine verbesserte Straßenlage und ein direkteres Fahrgefühl. Sie ist die richtige Wahl für alle, die Optik und Fahrdynamik gezielt aufwerten wollen und ihrem Fahrzeug einen kompromisslosen Performance-Look verleihen möchten.",
                Quantity = 10, Price = 1199.99m, ProductType = ProductType.Felge
            },
            new Product
            {
                ProductId = 21, CarId = 1, Name = "Pretoria Sport Felge", ArticleNumber = "100011",
                Brand = "Volkswagen", Description = "Die Pretoria Sport Felge kombiniert ein reduziertes, sportliches Design mit hoher Alltagstauglichkeit. Mit einem Durchmesser von 18 Zoll und einer Breite von 7,5 Zoll ist sie bewusst ausgewogen dimensioniert und eignet sich ideal für Fahrer, die sportliche Optik wollen, ohne Komfort und Effizienz zu opfern. Das klare Speichendesign wirkt leicht, modern und zeitlos.\n\nAls Volkswagen Original-Design fügt sich die Pretoria Sport Felge harmonisch in die Linienführung des Fahrzeugs ein. Sie verleiht dem Auto eine dezente, aber spürbare Aufwertung und passt sowohl zu sportlichen als auch zu eleganten Fahrzeugkonfigurationen.\n\nDank der moderaten Größe bietet diese Felge ein angenehmes Fahrverhalten, gute Alltagseigenschaften und eine zuverlässige Straßenlage. Die Pretoria Sport Felge ist damit die perfekte Wahl für Fahrer, die eine sportliche, aber vernünftige Lösung suchen.", 
                Quantity = 8,
                Price = 1349.99m, ProductType = ProductType.Felge
            },

            // Motoren
            new Product
            {
                ProductId = 3, CarId = 1, Name = "GTI 2.0 TFSI Performance Motor", ArticleNumber = "100002",
                Brand = "Volkswagen", Description = "Der GTI 2.0 TFSI Performance Motor steht für kompromisslose Leistung und moderne Volkswagen-Performance-Technologie. Mit 325 PS (239 kW) aus 2,0 Litern Hubraum liefert dieser Benzinmotor eine beeindruckende Kombination aus Durchzugskraft, Effizienz und sportlichem Charakter. Entwickelt für Fahrer, die maximale Performance erwarten – ohne Alltags­tauglichkeit einzubüßen.\n\nIn Verbindung mit dem 6-Gang-Automatikgetriebe sorgt der Motor für schnelle, präzise Gangwechsel und eine direkte Kraftentfaltung. Das Ergebnis ist ein dynamisches Fahrerlebnis mit souveräner Beschleunigung, hoher Laufruhe und klarer Kontrolle in jeder Fahrsituation.\n\nDer GTI 2.0 TFSI Performance Motor ist die ideale Wahl für sportlich ambitionierte Fahrer, die ein kraftvolles Upgrade suchen. Ob auf der Straße oder bei engagierter Fahrweise – dieser Motor liefert genau das, was der Name verspricht: echte GTI-Performance auf hohem technischen Niveau.", 
                Quantity = 5, Price = 2499.99m,
                ProductType = ProductType.Motor
            },
            new Product
            {
                ProductId = 4, CarId = 1, Name = "GTI Clubsport RS Motor", ArticleNumber = "100003",
                Brand = "Volkswagen", Description = "Der GTI Clubsport RS Motor markiert die Spitze der Volkswagen-Performanceklasse. Mit 360 PS (265 kW) aus 2,0 Litern Hubraum liefert dieser Benzinmotor eine kompromisslose Leistungsentfaltung, die klar auf sportliche Höchstansprüche ausgelegt ist. Dieses Aggregat richtet sich an Fahrer, die maximale Dynamik und ein spürbar aggressiveres Fahrgefühl suchen.\n\nDas 6-Gang-Automatikgetriebe sorgt für extrem schnelle Schaltvorgänge und eine direkte Umsetzung der Motorleistung auf die Straße. Die Kraftentfaltung erfolgt explosiv, gleichzeitig kontrolliert und präzise – ideal für ambitionierte Fahrweise und performanceorientierte Fahrzeugkonzepte.\n\nDer GTI Clubsport RS Motor ist kein Komfort-Upgrade, sondern ein echtes Performance-Statement. Entwickelt für Enthusiasten, die das Maximum aus ihrem Fahrzeug herausholen wollen und bewusst auf kompromisslose Leistung setzen.", 
                Quantity = 3,
                Price = 3399.99m, ProductType = ProductType.Motor
            },
            new Product
            {
                ProductId = 15, CarId = 1, Name = "GTI EcoBoost Hybrid Motor", ArticleNumber = "100008",
                Brand = "Volkswagen", Description = "Der GTI EcoBoost Hybrid Motor verbindet sportliche GTI-DNA mit moderner Hybridtechnologie. Mit 280 PS (210 kW) aus 1,8 Litern Hubraum bietet dieses Aggregat eine ausgewogene Kombination aus Leistung, Effizienz und zukunftsorientierter Antriebstechnik. Der Hybridantrieb sorgt für kraftvollen Durchzug bei gleichzeitig reduzierten Emissionen und verbessertem Verbrauch.\n\nIn Verbindung mit dem 6-Gang-Automatikgetriebe wird die Leistung gleichmäßig, direkt und komfortabel auf die Straße übertragen. Der elektrische Anteil unterstützt den Verbrennungsmotor besonders beim Anfahren und Beschleunigen, was zu einem spontanen, dynamischen Fahrgefühl führt – ohne die typische GTI-Sportlichkeit zu verlieren.\n\nDer GTI EcoBoost Hybrid Motor richtet sich an Fahrer, die Performance genießen wollen, dabei aber Wert auf Effizienz und moderne Technologie legen. Ein intelligentes Performance-Upgrade für alle, die sportliches Fahren mit einem Blick in die Zukunft verbinden möchten.", 
                Quantity = 4,
                Price = 3899.99m, ProductType = ProductType.Motor
            },

            // Lichter
            new Product
            {
                ProductId = 5, CarId = 1, Name = "IQ.Light LED Matrix Pro", ArticleNumber = "100004",
                Brand = "Volkswagen", Description = "Das IQ.Light LED Matrix Pro steht für modernste Lichttechnologie und maximale Sicherheit bei jeder Fahrbedingung. Mit einer Lichtleistung von 3.200 Lumen sorgt dieses LED-System für eine außergewöhnlich helle und gleichmäßige Ausleuchtung der Fahrbahn – ohne andere Verkehrsteilnehmer zu blenden. Die präzise Matrix-Technik passt den Lichtkegel intelligent an die Umgebung an.\n\nDank der vollwertigen LED-Technologie überzeugt das System durch schnelle Reaktionszeiten, hohe Energieeffizienz und eine deutlich längere Lebensdauer gegenüber herkömmlichen Scheinwerfern. Besonders bei Nachtfahrten, auf Landstraßen oder bei schlechten Sichtverhältnissen bietet IQ.Light einen spürbaren Sicherheitsgewinn.\n\nDas IQ.Light LED Matrix Pro ist die ideale Wahl für Fahrer, die höchsten Wert auf Sicht, Sicherheit und moderne Fahrzeugtechnik legen. Ein Premium-Upgrade, das Funktionalität und Hightech-Design perfekt miteinander verbindet.",
                Quantity = 10, Price = 1449.99m,
                ProductType = ProductType.Lichter
            },
            new Product
            {
                ProductId = 6, CarId = 1, Name = "Dynamic Vision LED Blackline", ArticleNumber = "100007",
                Brand = "Volkswagen", Description = "Das Dynamic Vision LED Blackline vereint moderne LED-Lichttechnik mit einer markant sportlichen Optik. Mit einer Lichtleistung von 3.000 Lumen sorgt dieses System für eine klare, gleichmäßige Ausleuchtung der Fahrbahn und verbessert die Sicht bei Nacht sowie bei schlechten Wetterbedingungen deutlich. Gleichzeitig verleiht das dunkle Blackline-Design dem Fahrzeug einen kraftvollen, hochwertigen Look.\n\nDie LED-Technologie bietet schnelle Reaktionszeiten, hohe Energieeffizienz und eine lange Lebensdauer. Das Lichtbild ist präzise abgestimmt und unterstützt sicheres Fahren, ohne andere Verkehrsteilnehmer unnötig zu blenden. Besonders auf Landstraßen und im Stadtverkehr zeigt sich der Vorteil der gleichmäßigen Ausleuchtung.\n\nDas Dynamic Vision LED Blackline ist die richtige Wahl für Fahrer, die Funktionalität und sportliches Design kombinieren möchten. Ein stilvolles Upgrade, das Sicherheit, moderne Technik und eine ausdrucksstarke Optik miteinander verbindet.", 
                Quantity = 8,
                Price = 1579.99m, ProductType = ProductType.Lichter
            },
            new Product
            {
                ProductId = 16, CarId = 1, Name = "NightDrive LED UltraBeam", ArticleNumber = "100009",
                Brand = "Volkswagen", Description = "Das NightDrive LED UltraBeam ist auf maximale Sichtleistung und höchste Sicherheit bei Nachtfahrten ausgelegt. Mit einer beeindruckenden Lichtleistung von 3.800 Lumen sorgt dieses LED-System für eine extrem helle, weitreichende Ausleuchtung der Fahrbahn und ermöglicht frühzeitiges Erkennen von Hindernissen, Fahrbahnmarkierungen und Verkehrszeichen.\n\nDank moderner LED-Technologie überzeugt das NightDrive UltraBeam durch ein präzises, klares Lichtbild, hohe Energieeffizienz und eine lange Lebensdauer. Besonders auf dunklen Landstraßen und bei schlechten Sichtverhältnissen spielt dieses System seine Stärken aus und bietet ein deutliches Plus an Sicherheit und Fahrkomfort.\n\nDas NightDrive LED UltraBeam richtet sich an Fahrer, die keine Kompromisse bei Sicht und Sicherheit eingehen möchten. Ein leistungsstarkes Premium-Upgrade für maximale Kontrolle und Vertrauen bei jeder Nachtfahrt.",
                Quantity = 6,
                Price = 1749.99m, ProductType = ProductType.Lichter
            },

            // Farben
            new Product
            {
                ProductId = 7, CarId = 1, Name = "Tornadorot", ArticleNumber = "100005", Brand = "Volkswagen",
                Description = "Kräftige Sportlackierung.", Quantity = 20, Price = 1799.99m, ProductType = ProductType.Lack
            },
            new Product
            {
                ProductId = 8, CarId = 1, Name = "Metallic Weiß Perleffekt", ArticleNumber = "100006",
                Brand = "Volkswagen", Description = "Eleganter Perlglanz.", Quantity = 20, Price = 1899.99m,
                ProductType = ProductType.Lack
            },
            new Product
            {
                ProductId = 17, CarId = 1, Name = "Deep Black Pearl", ArticleNumber = "100010", Brand = "Volkswagen",
                Description = "Tiefschwarze Premium-Lackierung.", Quantity = 15, Price = 2099.99m,
                ProductType = ProductType.Lack
            },

            // ================= POLO =================

            // Felgen
            new Product
            {
                ProductId = 22, CarId = 2, Name = "Astana Felge", ArticleNumber = "200010", Brand = "Volkswagen",
                Description = "Die Astana Felge ist auf maximale Alltagstauglichkeit und zeitloses Design ausgelegt. Mit einem Durchmesser von 16 Zoll und einer Breite von 6,5 Zoll bietet sie eine komfortorientierte Lösung für den täglichen Einsatz. Das geschlossene, aerodynamisch wirkende Design sorgt für eine ruhige, saubere Optik und fügt sich unauffällig in das Gesamtbild des Fahrzeugs ein.\n\nAls Volkswagen Originalfelge wurde die Astana speziell für Effizienz, Langlebigkeit und Fahrkomfort entwickelt. Sie eignet sich ideal für Stadtverkehr, Langstrecken und den ganzjährigen Einsatz. Die ausgewogene Größe unterstützt ein angenehmes Abrollverhalten und reduziert den Verschleiß von Reifen und Fahrwerk.\n\nDie Astana Felge ist die richtige Wahl für Fahrer, die Wert auf Zuverlässigkeit, Komfort und ein dezentes Erscheinungsbild legen – funktional, unaufdringlich und konsequent alltagstauglich.", 
                Quantity = 12, Price = 849.99m,
                ProductType = ProductType.Felge
            },
            new Product
            {
                ProductId = 23, CarId = 2, Name = "Bergamo Sport Felge", ArticleNumber = "200011", Brand = "Volkswagen",
                Description = "Die Bergamo Sport Felge verbindet sportliche Eleganz mit hoher Alltagstauglichkeit. Mit einem Durchmesser von 17 Zoll und einer Breite von 7,0 Zoll bietet sie einen ausgewogenen Kompromiss zwischen dynamischer Optik und komfortablem Fahrverhalten. Das geschwungene, kontrastreiche Speichendesign verleiht dem Fahrzeug eine moderne und hochwertige Ausstrahlung.\n\nAls Volkswagen Originalfelge ist die Bergamo Sport Felge perfekt auf die Fahrzeuge des Herstellers abgestimmt. Sie fügt sich harmonisch in das Gesamtbild ein und wertet sowohl kompakte Modelle als auch Mittelklassefahrzeuge sichtbar auf, ohne dabei aufdringlich zu wirken.\n\nDurch ihre moderate Dimension sorgt die Felge für ein stabiles Fahrgefühl, gute Effizienz und angenehmen Komfort im Alltag. Die Bergamo Sport Felge ist ideal für Fahrer, die sportliche Akzente setzen möchten, dabei aber nicht auf Alltagstkomfort verzichten wollen.", 
                Quantity = 10, Price = 979.99m,
                ProductType = ProductType.Felge
            },
            new Product
            {
                ProductId = 24, CarId = 2, Name = "Verona Black Performance Felge", ArticleNumber = "200012",
                Brand = "Volkswagen", Description = "Die Verona Black Performance Felge steht für einen kompromisslos sportlichen Auftritt mit klarer, kraftvoller Linienführung. Mit einem Durchmesser von 18 Zoll und einer Breite von 7,5 Zoll bietet sie eine ideale Balance aus Performance, Stabilität und Alltagstauglichkeit. Das kontrastreiche Schwarz-Design mit markanten Speichen sorgt für eine aggressive, moderne Optik und unterstreicht den sportlichen Charakter des Fahrzeugs.\n\nAls Volkswagen Performance Felge ist die Verona Black perfekt auf die technischen Anforderungen des Herstellers abgestimmt. Sie fügt sich nahtlos in sportliche Fahrzeugkonzepte ein und wirkt besonders stark bei dunklen Lackierungen, wo sie einen klaren Performance-Akzent setzt.\n\nDie ausgewogene Dimensionierung sorgt für präzises Lenkverhalten, gute Straßenlage und zuverlässigen Fahrkomfort. Die Verona Black Performance Felge ist die richtige Wahl für Fahrer, die ein sportliches Erscheinungsbild mit funktionaler Alltagstauglichkeit verbinden möchten.", 
                Quantity = 6, Price = 1099.99m,
                ProductType = ProductType.Felge
            },

            // Motoren
            new Product
            {
                ProductId = 11, CarId = 2, Name = "Polo 1.0 TSI BlueMotion Motor", ArticleNumber = "200003",
                Brand = "Volkswagen", Description = "Der Polo 1.0 TSI BlueMotion Motor ist auf Effizienz, Zuverlässigkeit und Alltagstauglichkeit ausgelegt. Mit 110 PS (81 kW) aus 1,0 Liter Hubraum bietet dieser Benzinmotor eine überraschend agile Leistungsentfaltung bei gleichzeitig niedrigem Verbrauch. Die BlueMotion-Technologie steht dabei für optimierte Effizienz und reduzierte Emissionen im täglichen Fahrbetrieb.\n\nIn Kombination mit dem 6-Gang-Schaltgetriebe ermöglicht der Motor eine direkte, kontrollierte Kraftübertragung und ein bewusst aktives Fahrerlebnis. Besonders im Stadt- und Pendelverkehr überzeugt das Aggregat durch seine Laufruhe, gute Elastizität und wirtschaftliche Charakteristik.\n\nDer Polo 1.0 TSI BlueMotion Motor ist die ideale Wahl für Fahrer, die ein zuverlässiges und sparsames Antriebskonzept suchen, ohne auf moderne Technik und solide Fahrleistungen verzichten zu wollen. Funktional, effizient und perfekt für den Alltag.", 
                Quantity = 8, Price = 1799.99m,
                ProductType = ProductType.Motor
            },
            new Product
            {
                ProductId = 12, CarId = 2, Name = "Polo 1.5 TSI GT-Line Motor", ArticleNumber = "200004",
                Brand = "Volkswagen", Description = "Der Polo 1.5 TSI GT-Line Motor bietet eine sportlich abgestimmte Leistungsreserve bei gleichzeitig hoher Effizienz. Mit 150 PS (110 kW) aus 1,5 Litern Hubraum positioniert sich dieser Benzinmotor deutlich über den klassischen Alltagsaggregaten und verleiht dem Polo ein spürbar dynamischeres Fahrverhalten.\n\nIn Kombination mit dem 6-Gang-Automatikgetriebe überzeugt der Motor durch schnelle, saubere Gangwechsel und eine gleichmäßige Kraftentfaltung. Beschleunigung, Durchzug und Laufruhe sind ausgewogen abgestimmt und machen den Polo sowohl im Stadtverkehr als auch auf der Autobahn souverän und agil.\n\nDer Polo 1.5 TSI GT-Line Motor richtet sich an Fahrer, die mehr Leistung und Sportlichkeit erwarten, ohne auf Komfort und Alltagstauglichkeit zu verzichten. Ein ideales Upgrade für alle, die den Polo deutlich dynamischer erleben möchten..",
                Quantity = 6, Price = 2399.99m,
                ProductType = ProductType.Motor
            },
            new Product
            {
                ProductId = 19, CarId = 2, Name = "Polo 1.2 Hybrid Drive", ArticleNumber = "200008",
                Brand = "Volkswagen", Description = "Der Polo 1.2 Hybrid Drive Motor ist konsequent auf Effizienz, Alltagstauglichkeit und moderne Antriebstechnologie ausgelegt. Mit 130 PS (96 kW) aus 1,2 Litern Hubraum kombiniert dieses Aggregat einen Benzinmotor mit Hybridunterstützung und bietet damit ein ausgewogenes Verhältnis aus Leistung, Verbrauch und Komfort.\n\nDas 6-Gang-Automatikgetriebe sorgt für sanfte, nahezu unmerkliche Gangwechsel und eine gleichmäßige Kraftentfaltung. Der Hybridantrieb unterstützt den Verbrennungsmotor besonders bei niedrigen Drehzahlen und im Stadtverkehr, was zu einem ruhigen Fahrgefühl, besserer Effizienz und reduziertem Verbrauch führt.\n\nDer Polo 1.2 Hybrid Drive Motor richtet sich an Fahrer, die ein modernes, sparsames Antriebskonzept suchen, ohne auf ausreichende Leistungsreserven zu verzichten. Eine clevere Wahl für den täglichen Einsatz mit Fokus auf Wirtschaftlichkeit und zeitgemäße Technik.",
                Quantity = 5,
                Price = 2599.99m, ProductType = ProductType.Motor
            },

            // Lichter
            new Product
            {
                ProductId = 13, CarId = 2, Name = "Polo LED Comfort Beam", ArticleNumber = "200005",
                Brand = "Volkswagen", Description = "Das Polo LED Comfort Beam ist auf angenehme Ausleuchtung und zuverlässige Alltagstauglichkeit ausgelegt. Mit einer Lichtleistung von 2.600 Lumen bietet dieses LED-System ein ausgewogenes, gleichmäßiges Lichtbild, das für gute Sicht bei Nacht und in der Dämmerung sorgt, ohne dabei zu blenden oder zu ermüden.\n\nDie moderne LED-Technologie garantiert eine hohe Energieeffizienz, lange Lebensdauer und sofortige Helligkeit beim Einschalten. Besonders im Stadtverkehr und auf täglichen Pendelstrecken überzeugt das Polo LED Comfort Beam durch seine ruhige Lichtcharakteristik und den spürbaren Komfortgewinn.\n\nDas Polo LED Comfort Beam ist die ideale Wahl für Fahrer, die ein zuverlässiges, komfortorientiertes Lichtsystem suchen. Funktional, effizient und perfekt auf den Alltag abgestimmt.",
                Quantity = 10, Price = 979.99m,
                ProductType = ProductType.Lichter
            },
            new Product
            {
                ProductId = 14, CarId = 2, Name = "Polo LED NightVision Plus", ArticleNumber = "200006",
                Brand = "Volkswagen", Description = "Das Polo LED NightVision Plus wurde für verbesserte Sicht und erhöhte Sicherheit bei Dunkelheit entwickelt. Mit einer Lichtleistung von 3.100 Lumen bietet dieses LED-System eine starke, gleichmäßige Ausleuchtung der Fahrbahn und erleichtert das Erkennen von Hindernissen, Fußgängern und Verkehrszeichen bei Nacht.\n\nDie moderne LED-Technologie sorgt für eine hohe Energieeffizienz, sofortige volle Helligkeit und eine lange Lebensdauer. Das präzise abgestimmte Lichtbild reduziert Ermüdung bei längeren Nachtfahrten und bietet ein spürbares Plus an Fahrkomfort, besonders auf schlecht beleuchteten Straßen.\n\nDas Polo LED NightVision Plus ist die ideale Wahl für Fahrer, die Wert auf Sicherheit, klare Sicht und moderne Lichttechnik legen. Ein hochwertiges Upgrade für souveränes und entspanntes Fahren bei Nacht.",
                Quantity = 8, Price = 1149.99m,
                ProductType = ProductType.Lichter
            },
            new Product
            {
                ProductId = 20, CarId = 2, Name = "Polo Adaptive Pixel Light", ArticleNumber = "200009",
                Brand = "Volkswagen", Description = "Das Polo Adaptive Pixel Light repräsentiert die fortschrittlichste Lichttechnologie in der Polo-Klasse. Mit einer Lichtleistung von 3.400 Lumen und adaptiver Pixelsteuerung passt dieses LED-System den Lichtkegel präzise an Fahrbahn, Verkehr und Umgebung an. Das Ergebnis ist maximale Sicht bei gleichzeitig minimaler Blendung anderer Verkehrsteilnehmer.\n\nDie intelligente Pixel-LED-Technologie ermöglicht eine dynamische Ausleuchtung, die sich in Echtzeit an Geschwindigkeit, Lenkwinkel und Verkehrssituation anpasst. Besonders bei Nachtfahrten, auf kurvigen Strecken oder bei schlechten Wetterbedingungen bietet dieses System einen deutlichen Sicherheits- und Komfortgewinn.\n\nDas Polo Adaptive Pixel Light ist die ideale Wahl für Fahrer, die modernste Lichttechnik, höchste Sicherheit und Premium-Komfort erwarten. Ein High-End-Upgrade für souveränes Fahren bei jeder Lichtsituation.",
                Quantity = 6,
                Price = 1349.99m, ProductType = ProductType.Lichter
            },

            // Farben
            new Product
            {
                ProductId = 9, CarId = 2, Name = "Crystal Ice Blue Metallic", ArticleNumber = "200001",
                Brand = "Volkswagen", Description = "Kühle Metallic-Lackierung.", Quantity = 20, Price = 1599.99m,
                ProductType = ProductType.Lack
            },
            new Product
            {
                ProductId = 10, CarId = 2, Name = "Kings Red Velvet", ArticleNumber = "200002", Brand = "Volkswagen",
                Description = "Edler Rotton mit Tiefenglanz.", Quantity = 20, Price = 1699.99m,
                ProductType = ProductType.Lack
            },
            new Product
            {
                ProductId = 18, CarId = 2, Name = "Urban Grey Metallic", ArticleNumber = "200007", Brand = "Volkswagen",
                Description = "Moderne urbane Graulackierung.", Quantity = 18, Price = 1649.99m,
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
                UnitPrice = 999.99m
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
                Description = "Der GTI Carbon Heckspoiler setzt ein klares sportliches Statement und unterstreicht den Performance-Charakter des Fahrzeugs bereits auf den ersten Blick. Gefertigt aus hochwertigem Carbon, überzeugt der Spoiler durch sein geringes Gewicht, hohe Stabilität und eine edle, motorsportnahe Optik.\n\nNeben der visuellen Aufwertung trägt der Heckspoiler zu einer verbesserten Aerodynamik bei, indem er den Abtrieb an der Hinterachse optimiert und das Fahrzeug bei höheren Geschwindigkeiten stabiler auf der Straße hält. Die präzise Formgebung fügt sich harmonisch in das Fahrzeugdesign ein und ergänzt die typische GTI-Linie perfekt.\n\nDer GTI Carbon Heckspoiler ist die ideale Wahl für Fahrer, die sportliche Optik und funktionalen Nutzen kombinieren möchten. Ein hochwertiges Performance-Upgrade, das Design, Technik und Fahrdynamik gekonnt miteinander verbindet.",
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
                Description = "Der Polo Sport Heckspoiler verleiht dem Polo eine dynamische, sportliche Optik und setzt gezielte Akzente am Heck des Fahrzeugs. Mit seiner kompakten, klaren Form fügt sich der Spoiler harmonisch in das Gesamtbild ein und unterstreicht den sportlichen Charakter, ohne überladen zu wirken.\n\nNeben der optischen Aufwertung unterstützt der Heckspoiler die Aerodynamik, indem er den Luftstrom am Fahrzeugheck sauberer führt und bei höheren Geschwindigkeiten für zusätzliche Stabilität sorgt. Die passgenaue Ausführung gewährleistet eine perfekte Integration in das originale Fahrzeugdesign.\n\nDer Polo Sport Heckspoiler ist ideal für Fahrer, die ihrem Fahrzeug einen sportlichen Look verleihen möchten, dabei aber Wert auf Alltagstauglichkeit und dezentes Design legen. Ein ausgewogenes Upgrade für Stil und Funktion.",
                Quantity = 7,
                Price = 720,
                ProductType = ProductType.Spoiler
            }
        );
    }

    int IDatabase.SaveChanges() => base.SaveChanges();
}