using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using PimpYourBlech_ClassLibrary.Exceptions;
using PimpYourBlech_ClassLibrary.Services.CustomerCommunication;
using PimpYourBlech_ClassLibrary.Services.Orders;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Query;
using PimpYourBlech_Data.Inventories;
using PimpYourBlech_Data.Models;

namespace PimpYourBlech_ClassLibrary.Services.Customers.Implementation;

public class CustomerService:ICustomerService
{
    private readonly ICustomerInventory _customerInventory;
    private readonly IOrderInventory _orderInventory;
    private readonly IEmailService _emailService;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(ICustomerInventory customerInventory, IOrderInventory orderInventory, IEmailService emailService, ILogger<CustomerService> logger)
    {
        _customerInventory = customerInventory;
        _orderInventory = orderInventory;
        _emailService = emailService;
        _logger = logger;
    }
    
    
    public async Task<List<CustomerDto>> GetListCustomersAsync()
    {
        var customers = await _customerInventory.ListCustomersAsync();
        return customers.ConvertAll(c => new CustomerDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            AdminRights = c.AdminRights,
            ImagePath = c.ImagePath,
            MailAddress = c.MailAddress,
            PasswordHash = c.PasswordHash,
            Telefon = c.Telefon,
            Username = c.Username,
        });
    }

    public async Task RegisterCustomerAsync(string firstName, string lastName, string username, string password,
        string passwordConfirm, string telefon,
        string mailAddress, string mailAddressConfirm, string ImagePath)
    {
        // Verfügbarkeit des Usernames checken
        await EnsureUsernameAvailableAsync(username);

        // Email Validierung
        _emailService.MailAdressChecker(mailAddress, mailAddressConfirm);


        // Passwörter auf Übereinstimmung checken
        EnsurePasswordAvailable(password, passwordConfirm);

        var hash = Convert.ToBase64String(
            SHA256.HashData(Encoding.UTF8.GetBytes(password ?? ""))
        );
        Customer customer = new Customer();
        customer.FirstName = firstName;
        customer.LastName = lastName;
        customer.Username = username;
        customer.PasswordHash = hash;
        customer.Telefon = telefon;
        customer.MailAddress = mailAddress;
        customer.ImagePath = ImagePath;
        await _customerInventory.InsertCustomerAsync(customer); 
       // await _emailService.SendRegistrationEmailAsync(customer);

        _logger.LogInformation(
            "Registering new customer with Username={Username}",
            username
        );
    }
    
    public async Task EnsureUsernameAvailableAsync(string username)
    {
        // Username darf nicht leer sein
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new WrongInputException("Username darf nicht leer sein.");
        }

        if (username.Length < 8)
        {
            throw new UsernameNotAvailableException("Username zu kurz. Mindestens 8 Zeichen.");
        }

        if (await _customerInventory.UsernameExistsAsync(username))
        {
            throw new UsernameNotAvailableException("Username ist bereits vergeben, bitte wähle einen anderen!");
        }
    }

    public void EnsurePasswordAvailable(string password, string passwordConfirm)
    {
        // Passwörter auf Übereinstimmung checken
        if (password != passwordConfirm)
        {
            throw new WrongPasswordException("Die Passwörter stimmen nicht überein.");
        }

        if (password.Length < 8)
        {
            throw new WrongPasswordException("Passwort zu kurz. Mindestens 8 Zeichen.");
        }
    }

    public async Task<CustomerDto> CustomerLoginAsync(string username, string password)
    {
        // Passwort wird in Hashcode konvertiert für DB - Abgleich
        var hash = Convert.ToBase64String(
            SHA256.HashData(Encoding.UTF8.GetBytes(password ?? ""))
        );

        Customer? customer = await _customerInventory.GetCustomerByUsernameAsync(username);

        if (customer == null)
        {
            throw new NoCustomerFoundException("Kein User mit diesem Username gefunden.");
        }

        if (customer.PasswordHash != hash)
        {
            throw new WrongPasswordException("Falsches Passwort. Bitte versuche es erneut.");
        }

        _logger.LogInformation(
            "Customer logged in successfully. CustomerId={CustomerId}, Username={Username}",
            customer.Id,
            customer.Username
        );

        return customer != null ? new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            AdminRights = customer.AdminRights,
            ImagePath = customer.ImagePath,
            MailAddress = customer.MailAddress,
            PasswordHash = customer.PasswordHash,
            Telefon = customer.Telefon,
            Username = customer.Username,
        } : null;
       
    }
    public async Task<List<DeliveryAddressDto>> GetUserAddressesAsync(int customerId)
    {
        var adresses = await _customerInventory.GetUserAddressesAsync(customerId);
        return adresses.ConvertAll(d => new DeliveryAddressDto
        {
            Id = d.Id,
            Country = d.Country,
            CustomerId = d.CustomerId,
            HouseNumber = d.HouseNumber,
            Lastname = d.Lastname,
            PostalCode = d.PostalCode,
            Salutation = d.Salutation,
            Street = d.Street,
            Surname = d.Surname,
            Town = d.Town,
        });
    }

    public async Task CanBeDeletedAsync(int customerId)
    {
        if ((await _orderInventory.GetOrdersForCustomerAsync(customerId)).Any())
        {
            throw new ForbiddenActionException(
                "User hat registrierte Bestellungen und darf daher nicht gelöscht werden.");
        }

        if ((await _customerInventory.GetUserAddressesAsync(customerId)).Any())
        {
            throw new ForbiddenActionException(
                "User hat registrierte Adressen und darf daher nicht gelöscht werden.");
        }
        
        if ((await _customerInventory.GetPaymentValuesAsync(customerId)).Any())
        {
            throw new ForbiddenActionException(
                "User hat registrierte Zahlungsmethoden und darf daher nicht gelöscht werden.");
        }
    }
    
     public async Task UpdateCustomerAsync(CustomerDto c)
    {
        Customer? customer = new Customer
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            AdminRights = c.AdminRights,
            ImagePath = c.ImagePath,
            MailAddress = c.MailAddress,
            PasswordHash = c.PasswordHash,
            Telefon = c.Telefon,
            Username = c.Username,
        };
        await _customerInventory.UpdateCustomerAsync(customer);
    }

    public async Task DeleteCustomerAsync(CustomerDto c)
    {
        
        
        Customer? customer = await _customerInventory.GetCustomerByIdAsync(c.Id);
        if (customer != null)
        {
            await _customerInventory.DeleteCustomerAsync(customer);
        }
    }

    public async Task<CustomerDto> GetCustomerByIdAsync(int id)
    {
        var customer = await _customerInventory.GetCustomerByIdAsync(id);
        return new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            AdminRights = customer.AdminRights,
            ImagePath = customer.ImagePath,
            MailAddress = customer.MailAddress,
            PasswordHash = customer.PasswordHash,
            Telefon = customer.Telefon,
            Username = customer.Username,
        };

    }

    public async Task<List<CustomerDto>> GetFilteredCustomersAsync(CustomerListQuery query)
    {
        var customers = await _customerInventory.QueryCustomersAsync(query);

        return customers.ConvertAll(c => new CustomerDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            AdminRights = c.AdminRights,
            ImagePath = c.ImagePath,
            MailAddress = c.MailAddress,
            PasswordHash = c.PasswordHash,
            Telefon = c.Telefon,
            Username = c.Username,
        });
    }
}