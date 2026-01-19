using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PimpYourBlech_Data.Migrations
{
    /// <inheritdoc />
    public partial class SeededDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "ArticleNumberSeq",
                startValue: 100026L);

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateProduction = table.Column<string>(type: "text", nullable: false),
                    DatePermit = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    PS = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommunityQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Telefon = table.Column<string>(type: "text", nullable: true),
                    MailAddress = table.Column<string>(type: "text", nullable: true),
                    AdminRights = table.Column<bool>(type: "boolean", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CarId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ArticleNumber = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    ProductType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommunityAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunityAnswers_CommunityQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "CommunityQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    CarId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configurations_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Configurations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    Salutation = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    HouseNumber = table.Column<string>(type: "text", nullable: false),
                    Town = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    AccountOwner = table.Column<string>(type: "text", nullable: false),
                    Iban = table.Column<string>(type: "text", nullable: false),
                    Bic = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentValues_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colors_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Fuel = table.Column<int>(type: "integer", nullable: false),
                    Ps = table.Column<int>(type: "integer", nullable: false),
                    Kw = table.Column<int>(type: "integer", nullable: false),
                    Displacement = table.Column<string>(type: "text", nullable: false),
                    Gear = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Engines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Lumen = table.Column<int>(type: "integer", nullable: false),
                    IsLed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lights_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    DiameterInInch = table.Column<decimal>(type: "numeric", nullable: false),
                    WidthInInch = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rims_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationProduct",
                columns: table => new
                {
                    ConfigurationsId = table.Column<int>(type: "integer", nullable: false),
                    ProductsProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationProduct", x => new { x.ConfigurationsId, x.ProductsProductId });
                    table.ForeignKey(
                        name: "FK_ConfigurationProduct_Configurations_ConfigurationsId",
                        column: x => x.ConfigurationsId,
                        principalTable: "Configurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigurationProduct_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    DeliveryAddressId = table.Column<int>(type: "integer", nullable: false),
                    PaymentValueId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryAddresses_DeliveryAddressId",
                        column: x => x.DeliveryAddressId,
                        principalTable: "DeliveryAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_PaymentValues_PaymentValueId",
                        column: x => x.PaymentValueId,
                        principalTable: "PaymentValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderPositions",
                columns: table => new
                {
                    OrderPositionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ArticleNumber = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPositions", x => x.OrderPositionId);
                    table.ForeignKey(
                        name: "FK_OrderPositions_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "DatePermit", "DateProduction", "Model", "Name", "PS", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Volkswagen", "01/2026", "10/2025", "GTI 2025", "Golf", 325, 44999.99m, 20 },
                    { 2, "Volkswagen", "01/2021", "11/2020", "Polo 2020", "Polo", 225, 24999.99m, 15 }
                });

            migrationBuilder.InsertData(
                table: "CommunityQuestions",
                columns: new[] { "Id", "Content", "CreatedAt" },
                values: new object[,]
                {
                    { 1, "Wie lange dauert die Lieferung eines Fahrzeugs?", new DateTime(2025, 1, 1, 4, 3, 0, 0, DateTimeKind.Utc) },
                    { 2, "Kann ich meine Konfiguration später ändern?", new DateTime(2025, 1, 1, 5, 3, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AdminRights", "FirstName", "ImagePath", "LastName", "MailAddress", "PasswordHash", "Telefon", "Username" },
                values: new object[,]
                {
                    { 1, true, "Max", "/CustomerImages/Car1.png", "Mustermann", "mustermail-admin.adresse@mustermail.de", "P6zbHsZ98YHkhf6yoM/EMMjAOt31qqUEdCRYJrKpKqs=", "0123456789", "MusterAdmin" },
                    { 2, false, "Max", "/CustomerImages/Car1.png", "Mustermann", "mustermail.adresse@mustermail.de", "P6zbHsZ98YHkhf6yoM/EMMjAOt31qqUEdCRYJrKpKqs=", "0123456789", "MusterMax" }
                });

            migrationBuilder.InsertData(
                table: "CommunityAnswers",
                columns: new[] { "Id", "Content", "CreatedAt", "QuestionId" },
                values: new object[,]
                {
                    { 1, "Die Lieferzeit beträgt in der Regel 4–6 Wochen.", new DateTime(2025, 1, 1, 6, 3, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, "Ja, gespeicherte Konfigurationen können jederzeit weiterbearbeitet werden.", new DateTime(2025, 1, 3, 5, 0, 0, 0, DateTimeKind.Utc), 2 }
                });

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "CarId", "CustomerId", "Name" },
                values: new object[,]
                {
                    { 1, 1, 1, "Max' Golf Konfiguration" },
                    { 2, 2, 1, "Max' Polo Konfiguration" },
                    { 3, 2, 2, "Mustermann Polo Setup" },
                    { 4, 1, 2, "Mustermann Golf Setup" }
                });

            migrationBuilder.InsertData(
                table: "DeliveryAddresses",
                columns: new[] { "Id", "Country", "CustomerId", "HouseNumber", "Lastname", "PostalCode", "Salutation", "Street", "Surname", "Town" },
                values: new object[,]
                {
                    { 1, "DE", 1, "12A", "Mustermann", "83022", "Herr", "Musterstraße", "Admin", "Rosenheim" },
                    { 2, "DE", 2, "12A", "Mustermann", "83022", "Herr", "Musterstraße", "Max", "Rosenheim" }
                });

            migrationBuilder.InsertData(
                table: "PaymentValues",
                columns: new[] { "Id", "AccountOwner", "Bic", "CustomerId", "Iban" },
                values: new object[] { 1, "Max Mustermann", "INGDDEFFXXX", 1, "DE44500105175407324931" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ArticleNumber", "Brand", "CarId", "Description", "ImageUrl", "Name", "Price", "ProductType", "Quantity" },
                values: new object[,]
                {
                    { 1, "0100000", "Volkswagen", 1, "Die Queenstown Felge steht für sportliches Design und solide Verarbeitung. Mit einem Durchmesser von 19 Zoll und einer Breite von 8 Zoll verleiht sie dem Fahrzeug eine kraftvolle, moderne Optik, ohne dabei übertrieben zu wirken. Das markante Mehrspeichen-Design sorgt für einen dynamischen Auftritt und lässt die Bremsanlage optisch größer und präsenter erscheinen.\n\nGefertigt für Volkswagen-Fahrzeuge, verbindet diese Felge Alltagstauglichkeit mit einem klaren Performance-Look. Sie eignet sich sowohl für den täglichen Einsatz als auch für Fahrer, die ihrem Fahrzeug eine deutlich aufgewertete Erscheinung verleihen wollen, ohne ins Extreme zu gehen.\n\nDank der ausgewogenen Dimensionen bietet die Queenstown Felge ein gutes Verhältnis aus Stabilität, Fahrkomfort und sportlicher Straßenlage. Eine ideale Wahl für alle, die Wert auf Qualität, saubere Optik und eine stimmige Gesamtwirkung legen.", null, "Queenstown Felge", 999.99m, 1, 10 },
                    { 2, "0100001", "Volkswagen", 1, "Die Warmenau Performance Felge richtet sich klar an Fahrer, die keine Kompromisse wollen. Mit einem großzügigen Durchmesser von 20 Zoll und einer Breite von 8,5 Zoll setzt sie ein deutlich sportliches Statement und unterstreicht den Performance-Charakter des Fahrzeugs schon im Stand. Das präzise, kantige Speichendesign wirkt technisch, aggressiv und hochwertig zugleich.\n\nEntwickelt für Volkswagen-Fahrzeuge, verbindet diese Felge modernes Motorsport-Design mit hoher Alltagstauglichkeit. Die klare Linienführung sorgt für eine starke Tiefenwirkung und bringt besonders bei dunklen Fahrzeugfarben ihre volle Wirkung zur Geltung.\n\nDurch die breitere Auslegung bietet die Warmenau Performance Felge eine verbesserte Straßenlage und ein direkteres Fahrgefühl. Sie ist die richtige Wahl für alle, die Optik und Fahrdynamik gezielt aufwerten wollen und ihrem Fahrzeug einen kompromisslosen Performance-Look verleihen möchten.", null, "Warmenau Performance Felge", 1199.99m, 1, 10 },
                    { 3, "0100002", "Volkswagen", 1, "Der GTI 2.0 TFSI Performance Motor steht für kompromisslose Leistung und moderne Volkswagen-Performance-Technologie. Mit 325 PS (239 kW) aus 2,0 Litern Hubraum liefert dieser Benzinmotor eine beeindruckende Kombination aus Durchzugskraft, Effizienz und sportlichem Charakter. Entwickelt für Fahrer, die maximale Performance erwarten – ohne Alltags­tauglichkeit einzubüßen.\n\nIn Verbindung mit dem 6-Gang-Automatikgetriebe sorgt der Motor für schnelle, präzise Gangwechsel und eine direkte Kraftentfaltung. Das Ergebnis ist ein dynamisches Fahrerlebnis mit souveräner Beschleunigung, hoher Laufruhe und klarer Kontrolle in jeder Fahrsituation.\n\nDer GTI 2.0 TFSI Performance Motor ist die ideale Wahl für sportlich ambitionierte Fahrer, die ein kraftvolles Upgrade suchen. Ob auf der Straße oder bei engagierter Fahrweise – dieser Motor liefert genau das, was der Name verspricht: echte GTI-Performance auf hohem technischen Niveau.", null, "GTI 2.0 TFSI Performance Motor", 2499.99m, 0, 5 },
                    { 4, "0100003", "Volkswagen", 1, "Der GTI Clubsport RS Motor markiert die Spitze der Volkswagen-Performanceklasse. Mit 360 PS (265 kW) aus 2,0 Litern Hubraum liefert dieser Benzinmotor eine kompromisslose Leistungsentfaltung, die klar auf sportliche Höchstansprüche ausgelegt ist. Dieses Aggregat richtet sich an Fahrer, die maximale Dynamik und ein spürbar aggressiveres Fahrgefühl suchen.\n\nDas 6-Gang-Automatikgetriebe sorgt für extrem schnelle Schaltvorgänge und eine direkte Umsetzung der Motorleistung auf die Straße. Die Kraftentfaltung erfolgt explosiv, gleichzeitig kontrolliert und präzise – ideal für ambitionierte Fahrweise und performanceorientierte Fahrzeugkonzepte.\n\nDer GTI Clubsport RS Motor ist kein Komfort-Upgrade, sondern ein echtes Performance-Statement. Entwickelt für Enthusiasten, die das Maximum aus ihrem Fahrzeug herausholen wollen und bewusst auf kompromisslose Leistung setzen.", null, "GTI Clubsport RS Motor", 3399.99m, 0, 3 },
                    { 5, "0100004", "Volkswagen", 1, "Das IQ.Light LED Matrix Pro steht für modernste Lichttechnologie und maximale Sicherheit bei jeder Fahrbedingung. Mit einer Lichtleistung von 3.200 Lumen sorgt dieses LED-System für eine außergewöhnlich helle und gleichmäßige Ausleuchtung der Fahrbahn – ohne andere Verkehrsteilnehmer zu blenden. Die präzise Matrix-Technik passt den Lichtkegel intelligent an die Umgebung an.\n\nDank der vollwertigen LED-Technologie überzeugt das System durch schnelle Reaktionszeiten, hohe Energieeffizienz und eine deutlich längere Lebensdauer gegenüber herkömmlichen Scheinwerfern. Besonders bei Nachtfahrten, auf Landstraßen oder bei schlechten Sichtverhältnissen bietet IQ.Light einen spürbaren Sicherheitsgewinn.\n\nDas IQ.Light LED Matrix Pro ist die ideale Wahl für Fahrer, die höchsten Wert auf Sicht, Sicherheit und moderne Fahrzeugtechnik legen. Ein Premium-Upgrade, das Funktionalität und Hightech-Design perfekt miteinander verbindet.", null, "IQ.Light LED Matrix Pro", 1449.99m, 2, 10 },
                    { 6, "0100005", "Volkswagen", 1, "Das Dynamic Vision LED Blackline vereint moderne LED-Lichttechnik mit einer markant sportlichen Optik. Mit einer Lichtleistung von 3.000 Lumen sorgt dieses System für eine klare, gleichmäßige Ausleuchtung der Fahrbahn und verbessert die Sicht bei Nacht sowie bei schlechten Wetterbedingungen deutlich. Gleichzeitig verleiht das dunkle Blackline-Design dem Fahrzeug einen kraftvollen, hochwertigen Look.\n\nDie LED-Technologie bietet schnelle Reaktionszeiten, hohe Energieeffizienz und eine lange Lebensdauer. Das Lichtbild ist präzise abgestimmt und unterstützt sicheres Fahren, ohne andere Verkehrsteilnehmer unnötig zu blenden. Besonders auf Landstraßen und im Stadtverkehr zeigt sich der Vorteil der gleichmäßigen Ausleuchtung.\n\nDas Dynamic Vision LED Blackline ist die richtige Wahl für Fahrer, die Funktionalität und sportliches Design kombinieren möchten. Ein stilvolles Upgrade, das Sicherheit, moderne Technik und eine ausdrucksstarke Optik miteinander verbindet.", null, "Dynamic Vision LED Blackline", 1579.99m, 2, 8 },
                    { 7, "0100006", "Volkswagen", 1, "Kräftige Sportlackierung.", null, "Tornadorot", 1799.99m, 4, 20 },
                    { 8, "0100007", "Volkswagen", 1, "Eleganter Perlglanz.", null, "Metallic Weiß Perleffekt", 1899.99m, 4, 20 },
                    { 9, "0100008", "Volkswagen", 2, "Kühle Metallic-Lackierung.", null, "Crystal Ice Blue Metallic", 1599.99m, 4, 20 },
                    { 10, "0100009", "Volkswagen", 2, "Edler Rotton mit Tiefenglanz.", null, "Kings Red Velvet", 1699.99m, 4, 20 },
                    { 11, "0100010", "Volkswagen", 2, "Der Polo 1.0 TSI BlueMotion Motor ist auf Effizienz, Zuverlässigkeit und Alltagstauglichkeit ausgelegt. Mit 110 PS (81 kW) aus 1,0 Liter Hubraum bietet dieser Benzinmotor eine überraschend agile Leistungsentfaltung bei gleichzeitig niedrigem Verbrauch. Die BlueMotion-Technologie steht dabei für optimierte Effizienz und reduzierte Emissionen im täglichen Fahrbetrieb.\n\nIn Kombination mit dem 6-Gang-Schaltgetriebe ermöglicht der Motor eine direkte, kontrollierte Kraftübertragung und ein bewusst aktives Fahrerlebnis. Besonders im Stadt- und Pendelverkehr überzeugt das Aggregat durch seine Laufruhe, gute Elastizität und wirtschaftliche Charakteristik.\n\nDer Polo 1.0 TSI BlueMotion Motor ist die ideale Wahl für Fahrer, die ein zuverlässiges und sparsames Antriebskonzept suchen, ohne auf moderne Technik und solide Fahrleistungen verzichten zu wollen. Funktional, effizient und perfekt für den Alltag.", null, "Polo 1.0 TSI BlueMotion Motor", 1799.99m, 0, 8 },
                    { 12, "0100011", "Volkswagen", 2, "Der Polo 1.5 TSI GT-Line Motor bietet eine sportlich abgestimmte Leistungsreserve bei gleichzeitig hoher Effizienz. Mit 150 PS (110 kW) aus 1,5 Litern Hubraum positioniert sich dieser Benzinmotor deutlich über den klassischen Alltagsaggregaten und verleiht dem Polo ein spürbar dynamischeres Fahrverhalten.\n\nIn Kombination mit dem 6-Gang-Automatikgetriebe überzeugt der Motor durch schnelle, saubere Gangwechsel und eine gleichmäßige Kraftentfaltung. Beschleunigung, Durchzug und Laufruhe sind ausgewogen abgestimmt und machen den Polo sowohl im Stadtverkehr als auch auf der Autobahn souverän und agil.\n\nDer Polo 1.5 TSI GT-Line Motor richtet sich an Fahrer, die mehr Leistung und Sportlichkeit erwarten, ohne auf Komfort und Alltagstauglichkeit zu verzichten. Ein ideales Upgrade für alle, die den Polo deutlich dynamischer erleben möchten..", null, "Polo 1.5 TSI GT-Line Motor", 2399.99m, 0, 6 },
                    { 13, "0100012", "Volkswagen", 2, "Das Polo LED Comfort Beam ist auf angenehme Ausleuchtung und zuverlässige Alltagstauglichkeit ausgelegt. Mit einer Lichtleistung von 2.600 Lumen bietet dieses LED-System ein ausgewogenes, gleichmäßiges Lichtbild, das für gute Sicht bei Nacht und in der Dämmerung sorgt, ohne dabei zu blenden oder zu ermüden.\n\nDie moderne LED-Technologie garantiert eine hohe Energieeffizienz, lange Lebensdauer und sofortige Helligkeit beim Einschalten. Besonders im Stadtverkehr und auf täglichen Pendelstrecken überzeugt das Polo LED Comfort Beam durch seine ruhige Lichtcharakteristik und den spürbaren Komfortgewinn.\n\nDas Polo LED Comfort Beam ist die ideale Wahl für Fahrer, die ein zuverlässiges, komfortorientiertes Lichtsystem suchen. Funktional, effizient und perfekt auf den Alltag abgestimmt.", null, "Polo LED Comfort Beam", 979.99m, 2, 10 },
                    { 14, "0100013", "Volkswagen", 2, "Das Polo LED NightVision Plus wurde für verbesserte Sicht und erhöhte Sicherheit bei Dunkelheit entwickelt. Mit einer Lichtleistung von 3.100 Lumen bietet dieses LED-System eine starke, gleichmäßige Ausleuchtung der Fahrbahn und erleichtert das Erkennen von Hindernissen, Fußgängern und Verkehrszeichen bei Nacht.\n\nDie moderne LED-Technologie sorgt für eine hohe Energieeffizienz, sofortige volle Helligkeit und eine lange Lebensdauer. Das präzise abgestimmte Lichtbild reduziert Ermüdung bei längeren Nachtfahrten und bietet ein spürbares Plus an Fahrkomfort, besonders auf schlecht beleuchteten Straßen.\n\nDas Polo LED NightVision Plus ist die ideale Wahl für Fahrer, die Wert auf Sicherheit, klare Sicht und moderne Lichttechnik legen. Ein hochwertiges Upgrade für souveränes und entspanntes Fahren bei Nacht.", null, "Polo LED NightVision Plus", 1149.99m, 2, 8 },
                    { 15, "0100014", "Volkswagen", 1, "Der GTI EcoBoost Hybrid Motor verbindet sportliche GTI-DNA mit moderner Hybridtechnologie. Mit 280 PS (210 kW) aus 1,8 Litern Hubraum bietet dieses Aggregat eine ausgewogene Kombination aus Leistung, Effizienz und zukunftsorientierter Antriebstechnik. Der Hybridantrieb sorgt für kraftvollen Durchzug bei gleichzeitig reduzierten Emissionen und verbessertem Verbrauch.\n\nIn Verbindung mit dem 6-Gang-Automatikgetriebe wird die Leistung gleichmäßig, direkt und komfortabel auf die Straße übertragen. Der elektrische Anteil unterstützt den Verbrennungsmotor besonders beim Anfahren und Beschleunigen, was zu einem spontanen, dynamischen Fahrgefühl führt – ohne die typische GTI-Sportlichkeit zu verlieren.\n\nDer GTI EcoBoost Hybrid Motor richtet sich an Fahrer, die Performance genießen wollen, dabei aber Wert auf Effizienz und moderne Technologie legen. Ein intelligentes Performance-Upgrade für alle, die sportliches Fahren mit einem Blick in die Zukunft verbinden möchten.", null, "GTI EcoBoost Hybrid Motor", 3899.99m, 0, 4 },
                    { 16, "0100015", "Volkswagen", 1, "Das NightDrive LED UltraBeam ist auf maximale Sichtleistung und höchste Sicherheit bei Nachtfahrten ausgelegt. Mit einer beeindruckenden Lichtleistung von 3.800 Lumen sorgt dieses LED-System für eine extrem helle, weitreichende Ausleuchtung der Fahrbahn und ermöglicht frühzeitiges Erkennen von Hindernissen, Fahrbahnmarkierungen und Verkehrszeichen.\n\nDank moderner LED-Technologie überzeugt das NightDrive UltraBeam durch ein präzises, klares Lichtbild, hohe Energieeffizienz und eine lange Lebensdauer. Besonders auf dunklen Landstraßen und bei schlechten Sichtverhältnissen spielt dieses System seine Stärken aus und bietet ein deutliches Plus an Sicherheit und Fahrkomfort.\n\nDas NightDrive LED UltraBeam richtet sich an Fahrer, die keine Kompromisse bei Sicht und Sicherheit eingehen möchten. Ein leistungsstarkes Premium-Upgrade für maximale Kontrolle und Vertrauen bei jeder Nachtfahrt.", null, "NightDrive LED UltraBeam", 1749.99m, 2, 6 },
                    { 17, "0100016", "Volkswagen", 1, "Tiefschwarze Premium-Lackierung.", null, "Deep Black Pearl", 2099.99m, 4, 15 },
                    { 18, "0100017", "Volkswagen", 2, "Moderne urbane Graulackierung.", null, "Urban Grey Metallic", 1649.99m, 4, 18 },
                    { 19, "0100018", "Volkswagen", 2, "Der Polo 1.2 Hybrid Drive Motor ist konsequent auf Effizienz, Alltagstauglichkeit und moderne Antriebstechnologie ausgelegt. Mit 130 PS (96 kW) aus 1,2 Litern Hubraum kombiniert dieses Aggregat einen Benzinmotor mit Hybridunterstützung und bietet damit ein ausgewogenes Verhältnis aus Leistung, Verbrauch und Komfort.\n\nDas 6-Gang-Automatikgetriebe sorgt für sanfte, nahezu unmerkliche Gangwechsel und eine gleichmäßige Kraftentfaltung. Der Hybridantrieb unterstützt den Verbrennungsmotor besonders bei niedrigen Drehzahlen und im Stadtverkehr, was zu einem ruhigen Fahrgefühl, besserer Effizienz und reduziertem Verbrauch führt.\n\nDer Polo 1.2 Hybrid Drive Motor richtet sich an Fahrer, die ein modernes, sparsames Antriebskonzept suchen, ohne auf ausreichende Leistungsreserven zu verzichten. Eine clevere Wahl für den täglichen Einsatz mit Fokus auf Wirtschaftlichkeit und zeitgemäße Technik.", null, "Polo 1.2 Hybrid Drive", 2599.99m, 0, 5 },
                    { 20, "0100019", "Volkswagen", 2, "Das Polo Adaptive Pixel Light repräsentiert die fortschrittlichste Lichttechnologie in der Polo-Klasse. Mit einer Lichtleistung von 3.400 Lumen und adaptiver Pixelsteuerung passt dieses LED-System den Lichtkegel präzise an Fahrbahn, Verkehr und Umgebung an. Das Ergebnis ist maximale Sicht bei gleichzeitig minimaler Blendung anderer Verkehrsteilnehmer.\n\nDie intelligente Pixel-LED-Technologie ermöglicht eine dynamische Ausleuchtung, die sich in Echtzeit an Geschwindigkeit, Lenkwinkel und Verkehrssituation anpasst. Besonders bei Nachtfahrten, auf kurvigen Strecken oder bei schlechten Wetterbedingungen bietet dieses System einen deutlichen Sicherheits- und Komfortgewinn.\n\nDas Polo Adaptive Pixel Light ist die ideale Wahl für Fahrer, die modernste Lichttechnik, höchste Sicherheit und Premium-Komfort erwarten. Ein High-End-Upgrade für souveränes Fahren bei jeder Lichtsituation.", null, "Polo Adaptive Pixel Light", 1349.99m, 2, 6 },
                    { 21, "0100020", "Volkswagen", 1, "Die Pretoria Sport Felge kombiniert ein reduziertes, sportliches Design mit hoher Alltagstauglichkeit. Mit einem Durchmesser von 18 Zoll und einer Breite von 7,5 Zoll ist sie bewusst ausgewogen dimensioniert und eignet sich ideal für Fahrer, die sportliche Optik wollen, ohne Komfort und Effizienz zu opfern. Das klare Speichendesign wirkt leicht, modern und zeitlos.\n\nAls Volkswagen Original-Design fügt sich die Pretoria Sport Felge harmonisch in die Linienführung des Fahrzeugs ein. Sie verleiht dem Auto eine dezente, aber spürbare Aufwertung und passt sowohl zu sportlichen als auch zu eleganten Fahrzeugkonfigurationen.\n\nDank der moderaten Größe bietet diese Felge ein angenehmes Fahrverhalten, gute Alltagseigenschaften und eine zuverlässige Straßenlage. Die Pretoria Sport Felge ist damit die perfekte Wahl für Fahrer, die eine sportliche, aber vernünftige Lösung suchen.", null, "Pretoria Sport Felge", 1349.99m, 1, 8 },
                    { 22, "0100021", "Volkswagen", 2, "Die Astana Felge ist auf maximale Alltagstauglichkeit und zeitloses Design ausgelegt. Mit einem Durchmesser von 16 Zoll und einer Breite von 6,5 Zoll bietet sie eine komfortorientierte Lösung für den täglichen Einsatz. Das geschlossene, aerodynamisch wirkende Design sorgt für eine ruhige, saubere Optik und fügt sich unauffällig in das Gesamtbild des Fahrzeugs ein.\n\nAls Volkswagen Originalfelge wurde die Astana speziell für Effizienz, Langlebigkeit und Fahrkomfort entwickelt. Sie eignet sich ideal für Stadtverkehr, Langstrecken und den ganzjährigen Einsatz. Die ausgewogene Größe unterstützt ein angenehmes Abrollverhalten und reduziert den Verschleiß von Reifen und Fahrwerk.\n\nDie Astana Felge ist die richtige Wahl für Fahrer, die Wert auf Zuverlässigkeit, Komfort und ein dezentes Erscheinungsbild legen – funktional, unaufdringlich und konsequent alltagstauglich.", null, "Astana Felge", 849.99m, 1, 12 },
                    { 23, "0100022", "Volkswagen", 2, "Die Bergamo Sport Felge verbindet sportliche Eleganz mit hoher Alltagstauglichkeit. Mit einem Durchmesser von 17 Zoll und einer Breite von 7,0 Zoll bietet sie einen ausgewogenen Kompromiss zwischen dynamischer Optik und komfortablem Fahrverhalten. Das geschwungene, kontrastreiche Speichendesign verleiht dem Fahrzeug eine moderne und hochwertige Ausstrahlung.\n\nAls Volkswagen Originalfelge ist die Bergamo Sport Felge perfekt auf die Fahrzeuge des Herstellers abgestimmt. Sie fügt sich harmonisch in das Gesamtbild ein und wertet sowohl kompakte Modelle als auch Mittelklassefahrzeuge sichtbar auf, ohne dabei aufdringlich zu wirken.\n\nDurch ihre moderate Dimension sorgt die Felge für ein stabiles Fahrgefühl, gute Effizienz und angenehmen Komfort im Alltag. Die Bergamo Sport Felge ist ideal für Fahrer, die sportliche Akzente setzen möchten, dabei aber nicht auf Alltagstkomfort verzichten wollen.", null, "Bergamo Sport Felge", 979.99m, 1, 10 },
                    { 24, "0100023", "Volkswagen", 2, "Die Verona Black Performance Felge steht für einen kompromisslos sportlichen Auftritt mit klarer, kraftvoller Linienführung. Mit einem Durchmesser von 18 Zoll und einer Breite von 7,5 Zoll bietet sie eine ideale Balance aus Performance, Stabilität und Alltagstauglichkeit. Das kontrastreiche Schwarz-Design mit markanten Speichen sorgt für eine aggressive, moderne Optik und unterstreicht den sportlichen Charakter des Fahrzeugs.\n\nAls Volkswagen Performance Felge ist die Verona Black perfekt auf die technischen Anforderungen des Herstellers abgestimmt. Sie fügt sich nahtlos in sportliche Fahrzeugkonzepte ein und wirkt besonders stark bei dunklen Lackierungen, wo sie einen klaren Performance-Akzent setzt.\n\nDie ausgewogene Dimensionierung sorgt für präzises Lenkverhalten, gute Straßenlage und zuverlässigen Fahrkomfort. Die Verona Black Performance Felge ist die richtige Wahl für Fahrer, die ein sportliches Erscheinungsbild mit funktionaler Alltagstauglichkeit verbinden möchten.", null, "Verona Black Performance Felge", 1099.99m, 1, 6 },
                    { 25, "0100024", "Volkswagen", 1, "Der GTI Carbon Heckspoiler setzt ein klares sportliches Statement und unterstreicht den Performance-Charakter des Fahrzeugs bereits auf den ersten Blick. Gefertigt aus hochwertigem Carbon, überzeugt der Spoiler durch sein geringes Gewicht, hohe Stabilität und eine edle, motorsportnahe Optik.\n\nNeben der visuellen Aufwertung trägt der Heckspoiler zu einer verbesserten Aerodynamik bei, indem er den Abtrieb an der Hinterachse optimiert und das Fahrzeug bei höheren Geschwindigkeiten stabiler auf der Straße hält. Die präzise Formgebung fügt sich harmonisch in das Fahrzeugdesign ein und ergänzt die typische GTI-Linie perfekt.\n\nDer GTI Carbon Heckspoiler ist die ideale Wahl für Fahrer, die sportliche Optik und funktionalen Nutzen kombinieren möchten. Ein hochwertiges Performance-Upgrade, das Design, Technik und Fahrdynamik gekonnt miteinander verbindet.", null, "GTI Carbon Heckspoiler", 950m, 17, 5 },
                    { 26, "0100025", "Volkswagen", 2, "Der Polo Sport Heckspoiler verleiht dem Polo eine dynamische, sportliche Optik und setzt gezielte Akzente am Heck des Fahrzeugs. Mit seiner kompakten, klaren Form fügt sich der Spoiler harmonisch in das Gesamtbild ein und unterstreicht den sportlichen Charakter, ohne überladen zu wirken.\n\nNeben der optischen Aufwertung unterstützt der Heckspoiler die Aerodynamik, indem er den Luftstrom am Fahrzeugheck sauberer führt und bei höheren Geschwindigkeiten für zusätzliche Stabilität sorgt. Die passgenaue Ausführung gewährleistet eine perfekte Integration in das originale Fahrzeugdesign.\n\nDer Polo Sport Heckspoiler ist ideal für Fahrer, die ihrem Fahrzeug einen sportlichen Look verleihen möchten, dabei aber Wert auf Alltagstauglichkeit und dezentes Design legen. Ein ausgewogenes Upgrade für Stil und Funktion.", null, "Polo Sport Heckspoiler", 720m, 17, 7 }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "DisplayName", "ProductId" },
                values: new object[,]
                {
                    { 1, "Tornadorot", 7 },
                    { 2, "Metallic Weiß Perleffekt", 8 },
                    { 3, "Deep Black Pearl", 17 },
                    { 4, "Crystal Ice Blue Metallic", 9 },
                    { 5, "Kings Red Velvet", 10 },
                    { 6, "Urban Grey Metallic", 18 }
                });

            migrationBuilder.InsertData(
                table: "Engines",
                columns: new[] { "Id", "Displacement", "Fuel", "Gear", "Kw", "ProductId", "Ps" },
                values: new object[,]
                {
                    { 1, "2.0", 0, 2, 239, 3, 325 },
                    { 2, "2.0", 0, 2, 265, 4, 360 },
                    { 3, "1.8", 3, 2, 210, 15, 280 },
                    { 4, "1.0", 0, 1, 81, 11, 110 },
                    { 5, "1.5", 0, 2, 110, 12, 150 },
                    { 6, "1.2", 3, 2, 96, 19, 130 }
                });

            migrationBuilder.InsertData(
                table: "Lights",
                columns: new[] { "Id", "IsLed", "Lumen", "ProductId" },
                values: new object[,]
                {
                    { 1, true, 3200, 5 },
                    { 2, true, 3000, 6 },
                    { 3, true, 3800, 16 },
                    { 4, true, 2600, 13 },
                    { 5, true, 3100, 14 },
                    { 6, true, 3400, 20 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "DeliveryAddressId", "OrderDate", "PaymentValueId", "TotalPrice" },
                values: new object[] { 1, 1, 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 2000m });

            migrationBuilder.InsertData(
                table: "Rims",
                columns: new[] { "Id", "DiameterInInch", "ProductId", "WidthInInch" },
                values: new object[,]
                {
                    { 1, 19m, 1, 8m },
                    { 2, 20m, 2, 8.5m },
                    { 3, 18m, 21, 7.5m },
                    { 4, 16m, 22, 6.5m },
                    { 5, 17m, 23, 7.0m },
                    { 6, 18m, 24, 7.5m }
                });

            migrationBuilder.InsertData(
                table: "OrderPositions",
                columns: new[] { "OrderPositionId", "ArticleNumber", "Brand", "Name", "OrderId", "Quantity", "Type", "UnitPrice" },
                values: new object[] { 1, "100000", "Volkswagen", "Queenstown Felge", 1, 2, 1, 999.99m });

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ProductId",
                table: "Colors",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommunityAnswers_QuestionId",
                table: "CommunityAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationProduct_ProductsProductId",
                table: "ConfigurationProduct",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_CarId",
                table: "Configurations",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_CustomerId",
                table: "Configurations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_CustomerId",
                table: "DeliveryAddresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_ProductId",
                table: "Engines",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lights_ProductId",
                table: "Lights",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderPositions_OrderId",
                table: "OrderPositions",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryAddressId",
                table: "Orders",
                column: "DeliveryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentValueId",
                table: "Orders",
                column: "PaymentValueId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentValues_CustomerId",
                table: "PaymentValues",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ArticleNumber",
                table: "Products",
                column: "ArticleNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CarId",
                table: "Products",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Rims_ProductId",
                table: "Rims",
                column: "ProductId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "CommunityAnswers");

            migrationBuilder.DropTable(
                name: "ConfigurationProduct");

            migrationBuilder.DropTable(
                name: "Engines");

            migrationBuilder.DropTable(
                name: "Lights");

            migrationBuilder.DropTable(
                name: "OrderPositions");

            migrationBuilder.DropTable(
                name: "Rims");

            migrationBuilder.DropTable(
                name: "CommunityQuestions");

            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "DeliveryAddresses");

            migrationBuilder.DropTable(
                name: "PaymentValues");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropSequence(
                name: "ArticleNumberSeq");
        }
    }
}
