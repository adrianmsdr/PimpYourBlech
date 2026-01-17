using Microsoft.Extensions.Logging;
using Moq;
using PimpYourBlech_ClassLibrary.Exceptions;
using PimpYourBlech_ClassLibrary.Services.Cars.Implementation;
using PimpYourBlech_ClassLibrary.Services.Configurator.Implementation;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Data.Inventories;
using PimpYourBlech_Data.Models;

namespace PimpYourBlech_Tests.ServiceTests;

[TestClass]
public class CarServiceTest
{
    // Mocks für alle Abhängigkeiten des Services
    private Mock<IProductInventory> _productInventoryMock;
    private Mock<ICarInventory> _carInventoryMock;
    private Mock<ILogger<CarService> > _loggerMock;

    private CarService _service;

    [TestInitialize]
    public void Setup()
    {
        // Initialisierung der Mocks
        _productInventoryMock = new Mock<IProductInventory>();
        _carInventoryMock = new Mock<ICarInventory>();
        _loggerMock = new Mock<ILogger<CarService>>();
        

        // Service wird mit gemockten Abhängigkeiten erstellt
        _service = new CarService(
            _carInventoryMock.Object,
            _productInventoryMock.Object,
            _loggerMock.Object
            
        );
    }
    
    [TestMethod]
    public async Task DeleteCarAsync_ThrowsException_WhenProductsExist()
    {
        // Arrange: Fahrzeug-Daten
        var carDto = new CarDto { Id = 1 };

        // Das Fahrzeug besitzt noch mindestens ein Produkt
        _productInventoryMock
            .Setup(p => p.GetProductsForCarsAsync(1))
            .ReturnsAsync(new List<Product> { new() });

        // Act & Assert: Löschen ist fachlich verboten
        await Assert.ThrowsExceptionAsync<ForbiddenActionException>(
            () => _service.DeleteCarAsync(carDto)
        );
    }
}