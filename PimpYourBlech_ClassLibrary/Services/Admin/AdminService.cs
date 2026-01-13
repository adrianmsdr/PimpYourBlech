using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Enums;
using PimpYourBlech_ClassLibrary.Exceptions;
using PimpYourBlech_ClassLibrary.Inventories;
using PimpYourBlech_ClassLibrary.Services.CustomerCommunication;

namespace PimpYourBlech_ClassLibrary.Services.Admin;

public class AdminService:IAdminService
{
    private readonly ICustomerInventory _customerRepository;
    private readonly IProductInventory _productRepository;
    private readonly ICarInventory _carRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<AdminService> _logger;
   
    public AdminService(ICustomerInventory costumers, IProductInventory products, ICarInventory car,  IEmailService email, ILogger<AdminService> logger)
    {
        _customerRepository  = costumers;
        _productRepository = products;
        _carRepository = car;
        _emailService = email;
        _logger = logger;
    }

    public async Task<List<Customer>> GetListCustomersAsync()
    {
        return await _customerRepository.ListCustomersAsync();
    }


    public async Task RegisterCustomerAsync(string firstName, string lastName, string username, string password, string passwordConfirm, string telefon,
        string mailAddress, string mailAddressConfirm,string ImagePath)
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
        await _customerRepository.InsertCustomerAsync(customer);
        await _emailService.SendRegistrationEmailAsync(customer);
        
        _logger.LogInformation(
            "Registering new customer with Username={Username}",
            username
        );
    }
    
    public Customer Login(string username, string passwordHash)
    {
        throw new NotImplementedException();
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

        if (await _customerRepository.UsernameExistsAsync(username))
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

    public async Task<Customer> CustomerLoginAsync(string username, string password)
    {


        // Passwort wird in Hashcode konvertiert für DB - Abgleich
        var hash = Convert.ToBase64String(
            SHA256.HashData(Encoding.UTF8.GetBytes(password ?? ""))
        );

        Customer?  customer = await _customerRepository.GetCustomerByUsernameAsync(username);

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


        return customer;

    }

    public async Task<Customer> GetCustomerByUsernameAsync(string username)
    {
        var temp = await _customerRepository.GetCustomerByUsernameAsync(username);

        return temp ?? throw new NoCustomerFoundException("Kein Benutzer mit diesen Username gefunden.");
    }

    public Customer GetCustomerByTelefon(string telefon)
    {
        var temp = _customerRepository.ListCustomers().FirstOrDefault(c => c.Telefon == telefon);

        return temp ?? throw new NoCustomerFoundException("Kein Benutzer mit diesen Username gefunden.");
    }

    public Customer GetCustomerByNames(string firstName, string lastName)
    {
        var temp = _customerRepository.ListCustomers()
            .FirstOrDefault(c => c.FirstName == firstName && c.LastName == lastName);

        return temp ?? throw new NoCustomerFoundException("Kein Benutzer mit diesen Namen gefunden.");

    }
    
    public async Task UpdateCustomerAsync(Customer c)
    {
        await _customerRepository.UpdateCustomerAsync(c);
    }
    
    public async Task DeleteCustomerAsync(Customer c)
    {
        await _customerRepository.DeleteCustomerAsync(c);
    }

    public async Task<Customer> GetCustomerByIdIncludeAllAsync(int id)
    {
        return await _customerRepository.GetCustomerByIdIncludeAllAsync(id);
    }
    
    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        return await _customerRepository.GetCustomerByIdAsync(id);
    }
    
    
    // ___________________________________Poducts____________________________________
    public Product CreateProduct(Car car, string name, string brand, int quantity, decimal price,
        ProductType productType, string description)
    {
        _logger.LogInformation(
            "Product instance created. Name={Name}, CarId={CarId}, Type={Type}",
            name,
            car.Id,
            productType
        );

        return new Product
        {
            Car = car,
            CarId = car.Id,
            Name = name,
            Brand = brand,
            Quantity = quantity,
            Price = price,
            ProductType = productType,
            Description = description
        };
    }

    public void RegisterEngine(Product p, int ps, int kw, string displacement, Gear gear, Fuel fuel)
    {
        p.EngineDetail = new EngineDetail
        {
            ProductId = p.ProductId,
            Ps = ps,
            Kw = kw,
            Displacement = displacement,
            Gear = gear,
            Fuel = fuel
        };
        _logger.LogInformation("Engine registered for product {ProductId}.", p.ProductId);
    }

    public void RegisterRim(Product p, decimal diameter, decimal width)
    {
        p.RimDetail = new RimDetail
        {
            ProductId = p.ProductId,
            DiameterInInch = diameter,
            WidthInInch = width
        };
        _logger.LogInformation("Rim registered for product {ProductId}.", p.ProductId);
    }

    public void RegisterLights(Product p, int lumen, bool isLED)
    {
        p.LightsDetail = new LightsDetail
        {
            ProductId = p.ProductId,
            Lumen = lumen,
            IsLed = isLED
        };
        _logger.LogInformation("Light registered for product {ProductId}", p.ProductId);
    }

    public void RegisterColor(Product p, string colorName)
    {
        p.ColorDetail = new ColorDetail
        {
            ProductId = p.ProductId,
            DisplayName = colorName,
        };
        _logger.LogInformation("Color registered for product {ProductId}.", p.ProductId);
    }

    public async Task<Product> InsertProduct(Product p)
    {
        await _productRepository.InsertProductAsync(p); // muss SaveChangesAsync machen
        _logger.LogInformation($"Inserted product {p.ProductId}");
        return p;
    }

    public List<Product> GetProducts()
    {
        return _productRepository.ListProducts();
    }
    
    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetProductByIdAsync(id);
    }

    public async Task DeleteProductAsync(Product p)
    {
        await _productRepository.DeleteProductAsync(p);
        _logger.LogWarning("Product deleted");
    }

    public async Task UpdateProductAsync(Product p)
    {
        await _productRepository.UpdateProductAsync(p);
        _logger.LogInformation("Product updated");
    }
    
    public List<ProductType> GetProductTypes()
    {
        return Enum.GetValues(typeof(ProductType)).Cast<ProductType>().ToList();
    }

    // ___________________________________Cars____________________________________


    public async Task<List<Car>> GetCarsAsync()
    {
        return await _carRepository.ListCarsAsync();
    }
    public async Task<Car> RegisterCarAsync(string name, string dateProduction, string datePermit, string brand,
        string model, int ps, int quantity,
        decimal price)
    {
        Car c = new Car();
        c.Name = name;
        c.DateProduction = dateProduction;
        c.DatePermit = datePermit;
        c.Brand = brand;
        c.Model = model;
        c.PS = ps;
        c.Quantity = quantity;
        c.Price = price;
        await _carRepository.InsertCarAsync(c);
        return c;
    }
    
    public async Task<Car> GetCarByIdAsync(int id)
    {
        return await _carRepository.GetCarByIdAsync(id);
    }

    public async Task DeleteCarAsync(Car car)
    {
        await _carRepository.DeleteCarAsync(car);
        _logger.LogInformation("Car deleted");
    }

    public async Task UpdateCarAsync(Car car)
    {
        await  _carRepository.UpdateCarAsync(car);
        _logger.LogInformation(
            "Car updated. CarId={CarId}, Name={Name}",
            car.Id,
            car.Name
        );

    }
    
    public List<Product> GetAvailableRims(int carId)
    {
        return GetProducts()
            .Where(p => p.CarId == carId && p.ProductType == ProductType.Felge)
            .ToList();
    }

    public List<Product> GetAvailableColors(int carId)
    {
        return GetProducts()
            .Where(p => p.CarId == carId && p.ProductType == ProductType.Lack)
            .ToList();
    }

    // ___________________________________Orders____________________________________

    public  async Task<List<Order>> GetOrdersAsync()
    {
        return await _customerRepository.GetOrdersAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        return await _customerRepository.GetOrderByIdAsync(id);
    }

    public async Task<List<OrderPosition>> GetOrderItemsAsync(int id)
    {
        return await _customerRepository.GetOrderItemsAsync(id);
    }
}