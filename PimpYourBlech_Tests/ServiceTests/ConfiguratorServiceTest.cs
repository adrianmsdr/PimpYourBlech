using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PimpYourBlech_ClassLibrary.Services.Configurator.Implementation;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Enums;
using PimpYourBlech_Data.Inventories;
using PimpYourBlech_Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

/* Hier wird mit Moq ein Objekt zur Laufzeit erzeugt, das:

    -das Interface implementiert

    -exakt das zurückgibt, was du vorgibst

    -Aufrufe protokolliert

 ((  dotnet add package Moq )) */

namespace PimpYourBlech_Tests.Services.Configurator;

[TestClass]
public class ConfiguratorServiceTests
{
    // Mocks für alle Abhängigkeiten des Services
    private Mock<IConfigurationInventory> _configurationInventoryMock;
    private Mock<ICustomerInventory> _customerInventoryMock;
    private Mock<IProductInventory> _productInventoryMock;
    private Mock<ICarInventory> _carInventoryMock;

    private ConfiguratorService _service;

    [TestInitialize]
    public void Setup()
    {
        // Initialisierung der Mocks
        _configurationInventoryMock = new Mock<IConfigurationInventory>();
        _customerInventoryMock = new Mock<ICustomerInventory>();
        _productInventoryMock = new Mock<IProductInventory>();
        _carInventoryMock = new Mock<ICarInventory>();

        // Service wird mit gemockten Abhängigkeiten erstellt
        _service = new ConfiguratorService(
            _customerInventoryMock.Object,
            _productInventoryMock.Object,
            _carInventoryMock.Object,
            _configurationInventoryMock.Object
        );
    }

    [TestMethod]
    public async Task GetAllConfigurationsForCustomerAsync_CalculatesTotalPriceAndTotalPsCorrectly()
    {
        // ---------------- Arrange ----------------
        // Testkonfiguration mit Fahrzeug und Produkten
        var configuration = new Configuration
        {
            Id = 1,
            CustomerId = 42,
            Car = new Car
            {
                PS = 300,
                Price = 50000,
                Quantity = 1
            },
            Products = new List<Product>
            {
                // Motor mit eigener Leistung
                new Product
                {
                    ProductType = ProductType.Motor,
                    Price = 8000,
                    EngineDetail = new EngineDetail { Ps = 420 }
                },
                // Zusätzliches Produkt
                new Product
                {
                    ProductType = ProductType.Felge,
                    Price = 2000
                }
            }
        };

        // Inventory liefert genau diese Konfiguration zurück
        _configurationInventoryMock
            .Setup(i => i.GetConfigurationsIncludeCarProductsAsync(42))
            .ReturnsAsync(new List<Configuration> { configuration });

        // ---------------- Act ----------------
        // Aufruf der zu testenden Service-Methode
        var result = await _service.GetAllConfigurationsForCustomerAsync(42);

        // ---------------- Assert ----------------
        // Eine Konfiguration wurde zurückgegeben
        Assert.AreEqual(1, result.Count);

        var dto = result[0];

        // Gesamtpreis = Fahrzeugpreis + Summe der Produktpreise
        Assert.AreEqual(60000, dto.TotalPrice);

        // Motorleistung ersetzt die Basisleistung des Fahrzeugs
        Assert.AreEqual(420, dto.TotalPs);

        // Anzahl der Produkte korrekt gezählt
        Assert.AreEqual(2, dto.ProductCount);
    }
}
