using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PimpYourBlech_Data.Migrations
{
    /// <inheritdoc />
    public partial class migrationpyb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "ArticleNumberSeq",
                startValue: 100000L);

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
                    { 1, "Volkswagen", "2026", "2025", "GTI 2025", "Golf", 325, 45000m, 20 },
                    { 2, "Volkswagen", "2021", "2020", "Polo 2020", "Polo", 225, 25000m, 15 }
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
                table: "Configurations",
                columns: new[] { "Id", "CarId", "CustomerId", "Name" },
                values: new object[,]
                {
                    { 1, 1, 1, "Max' Golf Konfiguration" },
                    { 2, 2, 2, "Mustermann Polo Setup" }
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
                    { 1, "100000", "Volkswagen", 1, "Sportliche Alufelge mit roten Akzenten.", null, "Queenstown Felge", 1000m, 1, 10 },
                    { 2, "100001", "Volkswagen", 1, "Aerodynamische Premium-Felge für sportliche Fahrweise.", null, "Warmenau Performance Felge", 1200m, 1, 10 },
                    { 3, "100002", "Volkswagen", 1, "325 PS Turbo-Benzinmotor.", null, "GTI 2.0 TFSI Performance Motor", 2500m, 0, 5 },
                    { 4, "100003", "Volkswagen", 1, "Hochleistungsmotor mit Rennsportabstimmung.", null, "GTI Clubsport RS Motor", 3400m, 0, 3 },
                    { 5, "100004", "Volkswagen", 1, "Adaptive Matrix-LED-Scheinwerfer.", null, "IQ.Light LED Matrix Pro", 1450m, 2, 10 },
                    { 6, "100007", "Volkswagen", 1, "Sportlicher LED-Scheinwerfer mit dunklem Gehäuse.", null, "Dynamic Vision LED Blackline", 1580m, 2, 8 },
                    { 7, "100005", "Volkswagen", 1, "Kräftige Sportlackierung.", null, "Tornadorot", 1800m, 4, 20 },
                    { 8, "100006", "Volkswagen", 1, "Eleganter Perlglanz.", null, "Metallic Weiß Perleffekt", 1900m, 4, 20 },
                    { 9, "200001", "Volkswagen", 2, "Kühle Metallic-Lackierung.", null, "Crystal Ice Blue Metallic", 1600m, 4, 20 },
                    { 10, "200002", "Volkswagen", 2, "Edler Rotton mit Tiefenglanz.", null, "Kings Red Velvet", 1700m, 4, 20 },
                    { 11, "200003", "Volkswagen", 2, "Effizienter Stadtturbomotor.", null, "Polo 1.0 TSI BlueMotion Motor", 1800m, 0, 8 },
                    { 12, "200004", "Volkswagen", 2, "Sportlicher Turbomotor.", null, "Polo 1.5 TSI GT-Line Motor", 2400m, 0, 6 },
                    { 13, "200005", "Volkswagen", 2, "Gleichmäßige LED-Ausleuchtung.", null, "Polo LED Comfort Beam", 980m, 2, 10 },
                    { 14, "200006", "Volkswagen", 2, "Erweiterte Reichweite bei Nacht.", null, "Polo LED NightVision Plus", 1150m, 2, 8 },
                    { 15, "100008", "Volkswagen", 1, "Effizienter Hybridmotor mit ruhigem Lauf.", null, "GTI EcoBoost Hybrid Motor", 3900m, 0, 4 },
                    { 16, "100009", "Volkswagen", 1, "Extrem helle LED-Scheinwerfer für maximale Sicht.", null, "NightDrive LED UltraBeam", 1750m, 2, 6 },
                    { 17, "100010", "Volkswagen", 1, "Tiefschwarze Premium-Lackierung.", null, "Deep Black Pearl", 2100m, 4, 15 },
                    { 18, "200007", "Volkswagen", 2, "Moderne urbane Graulackierung.", null, "Urban Grey Metallic", 1650m, 4, 18 },
                    { 19, "200008", "Volkswagen", 2, "Hybridantrieb mit niedrigem Verbrauch.", null, "Polo 1.2 Hybrid Drive", 2600m, 0, 5 },
                    { 20, "200009", "Volkswagen", 2, "Blendfreies Fernlicht mit Pixel-Technologie.", null, "Polo Adaptive Pixel Light", 1350m, 2, 6 },
                    { 21, "100011", "Volkswagen", 1, "Leichte Schmiedefelge mit hoher Stabilität.", null, "Pretoria Sport Felge", 1350m, 1, 8 },
                    { 22, "200010", "Volkswagen", 2, "Klassische Alufelge für Alltag und Komfort.", null, "Astana Felge", 850m, 1, 12 },
                    { 23, "200011", "Volkswagen", 2, "Sportliche Mehrspeichenfelge.", null, "Bergamo Sport Felge", 980m, 1, 10 },
                    { 24, "200012", "Volkswagen", 2, "Schwarze Performance-Felge.", null, "Verona Black Performance Felge", 1100m, 1, 6 },
                    { 30, "300001", "Volkswagen", 1, "Sportlicher Carbon-Heckspoiler für verbesserte Aerodynamik.", null, "GTI Carbon Heckspoiler", 950m, 17, 5 },
                    { 31, "300002", "Volkswagen", 2, "Kompakter Sportspoiler für den Polo.", null, "Polo Sport Heckspoiler", 720m, 17, 7 }
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
                values: new object[] { 1, "100000", "Volkswagen", "Queenstown Felge", 1, 2, 1, 1000m });

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
