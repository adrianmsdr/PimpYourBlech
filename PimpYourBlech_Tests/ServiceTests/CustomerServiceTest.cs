using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using Moq;
using PimpYourBlech_ClassLibrary.Exceptions;
using PimpYourBlech_ClassLibrary.Services.CustomerCommunication;
using PimpYourBlech_ClassLibrary.Services.Customers.Implementation;
using PimpYourBlech_ClassLibrary.Services.Products.Implememtation;
using PimpYourBlech_Data.Inventories;
using PimpYourBlech_Data.Models;

namespace PimpYourBlech_Tests.ServiceTests;

[TestClass]
public class CustomerServiceTest
{
    // Mocks für alle Abhängigkeiten des AdminService
    private Mock<ICustomerInventory> _customerInventoryMock;
    private Mock<IEmailService> _emailServiceMock;
    private Mock<ILogger<CustomerService>> _loggerCustomerMock;
    private Mock<IOrderInventory> _orderInventoryMock;

    private CustomerService _service;

    [TestInitialize]
    public void Setup()
    {
        // Initialisierung der Mocks
        _customerInventoryMock = new Mock<ICustomerInventory>();
        _emailServiceMock = new Mock<IEmailService>();
        _loggerCustomerMock = new Mock<ILogger<CustomerService>>();
        _orderInventoryMock = new Mock<IOrderInventory>();
        

        // Service wird mit gemockten Abhängigkeiten erstellt
        _service = new CustomerService(
            _customerInventoryMock.Object,
            _orderInventoryMock.Object,
            _emailServiceMock.Object,
            _loggerCustomerMock.Object
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