using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Logging;
using PimpYourBlech_ClassLibrary.Services.Admin;
using PimpYourBlech_ClassLibrary.Exceptions;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Data.Inventories;
using PimpYourBlech_Data.Models;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PimpYourBlech_ClassLibrary.Services.CustomerCommunication;

/* Hier wird mit Moq ein Objekt zur Laufzeit erzeugt, das:

    -das Interface implementiert

    -exakt das zurückgibt, was du vorgibst

    -Aufrufe protokolliert

 ((  dotnet add package Moq )) */

namespace PimpYourBlech_Tests.Services.Admin;

[TestClass]
public class AdminServiceTests
{
    // Mocks für alle Abhängigkeiten des AdminService
    private Mock<ICustomerInventory> _customerInventoryMock;
    private Mock<IProductInventory> _productInventoryMock;
    private Mock<ICarInventory> _carInventoryMock;
    private Mock<IEmailService> _emailServiceMock;
    private Mock<ILogger<AdminService>> _loggerMock;

    private AdminService _service;

    [TestInitialize]
    public void Setup()
    {
        // Initialisierung der Mocks
        _customerInventoryMock = new Mock<ICustomerInventory>();
        _productInventoryMock = new Mock<IProductInventory>();
        _carInventoryMock = new Mock<ICarInventory>();
        _emailServiceMock = new Mock<IEmailService>();
        _loggerMock = new Mock<ILogger<AdminService>>();

        // Service wird mit gemockten Abhängigkeiten erstellt
        _service = new AdminService(
            _customerInventoryMock.Object,
            _productInventoryMock.Object,
            _carInventoryMock.Object,
            _emailServiceMock.Object,
            _loggerMock.Object
        );
    }

    // ------------------------------------------------------
    // DeleteCarAsync
    // ------------------------------------------------------

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

    // ------------------------------------------------------
    // EnsurePasswordAvailable
    // ------------------------------------------------------

    [TestMethod]
    public void EnsurePasswordAvailable_ThrowsException_WhenPasswordsDoNotMatch()
    {
        // Passwörter stimmen nicht überein
        Assert.ThrowsException<WrongPasswordException>(() =>
            _service.EnsurePasswordAvailable("password123", "password456")
        );
    }

    [TestMethod]
    public void EnsurePasswordAvailable_ThrowsException_WhenPasswordTooShort()
    {
        // Passwort ist kürzer als die erlaubte Mindestlänge
        Assert.ThrowsException<WrongPasswordException>(() =>
            _service.EnsurePasswordAvailable("short", "short")
        );
    }

    // ------------------------------------------------------
    // CustomerLoginAsync
    // ------------------------------------------------------

    [TestMethod]
    public async Task CustomerLoginAsync_ReturnsCustomer_WhenCredentialsAreValid()
    {
        // Arrange: Passwort und Hash wie im echten Login
        var password = "securePassword";
        var hash = Convert.ToBase64String(
            SHA256.HashData(Encoding.UTF8.GetBytes(password))
        );

        // Kunde existiert mit passendem Hash
        var customer = new Customer
        {
            Id = 5,
            Username = "testuser",
            PasswordHash = hash
        };

        _customerInventoryMock
            .Setup(c => c.GetCustomerByUsernameAsync("testuser"))
            .ReturnsAsync(customer);

        // Act: Login ausführen
        var result = await _service.CustomerLoginAsync("testuser", password);

        // Assert: Login erfolgreich
        Assert.IsNotNull(result);
        Assert.AreEqual(customer.Id, result.Id);
        Assert.AreEqual(customer.Username, result.Username);
    }

    [TestMethod]
    public async Task CustomerLoginAsync_ThrowsException_WhenPasswordIsWrong()
    {
        // Kunde existiert, Passwort-Hash passt aber nicht
        var customer = new Customer
        {
            Username = "testuser",
            PasswordHash = "invalidHash"
        };

        _customerInventoryMock
            .Setup(c => c.GetCustomerByUsernameAsync("testuser"))
            .ReturnsAsync(customer);

        // Falsches Passwort muss eine Exception auslösen
        await Assert.ThrowsExceptionAsync<WrongPasswordException>(() =>
            _service.CustomerLoginAsync("testuser", "wrongPassword")
        );
    }
}