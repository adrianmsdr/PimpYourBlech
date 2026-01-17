using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;
using PimpYourBlech_ClassLibrary.Exceptions;
using PimpYourBlech_Data.Inventories;
using PimpYourBlech_ClassLibrary.Services.CustomerCommunication;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Enums;
using PimpYourBlech_Contracts.Query;
using PimpYourBlech_Data.Models;

namespace PimpYourBlech_ClassLibrary.Services.Admin;

public class AdminService : IAdminService
{
    private readonly ICustomerInventory _customerRepository;
    private readonly IProductInventory _productRepository;
    private readonly ICarInventory _carRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<AdminService> _logger;
    

    public AdminService(ICustomerInventory costumers, IProductInventory products, ICarInventory car,
        IEmailService email, ILogger<AdminService> logger)
    {
        _customerRepository = costumers;
        _productRepository = products;
        _carRepository = car;
        _emailService = email;
        _logger = logger;
    }

    public async Task<List<CustomerDto>> GetListCustomersAsync()
    {
        var customers = await _customerRepository.ListCustomersAsync();
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
        await _customerRepository.InsertCustomerAsync(customer);
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

    public async Task<CustomerDto> CustomerLoginAsync(string username, string password)
    {
        // Passwort wird in Hashcode konvertiert für DB - Abgleich
        var hash = Convert.ToBase64String(
            SHA256.HashData(Encoding.UTF8.GetBytes(password ?? ""))
        );

        Customer? customer = await _customerRepository.GetCustomerByUsernameAsync(username);

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
        await _customerRepository.UpdateCustomerAsync(customer);
    }

    public async Task DeleteCustomerAsync(CustomerDto c)
    {
        
        
        Customer? customer = await _customerRepository.GetCustomerByIdAsync(c.Id);
        if (customer != null)
        {
            await _customerRepository.DeleteCustomerAsync(customer);
        }
    }
/*
    public async Task<CustomerDto> GetCustomerByIdIncludeAllAsync(int id)
    {
       // return await _customerRepository.GetCustomerByIdIncludeAllAsync(id);
    }
*/
    public async Task<CustomerDto> GetCustomerByIdAsync(int id)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(id);
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
        var customers = await _customerRepository.QueryCustomersAsync(query);

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


    // ___________________________________Poducts____________________________________
    public async Task<int> CreateProduct(CarDto car, string name, string brand, int quantity, decimal price,
        ProductType productType, string description)
    {
        _logger.LogInformation(
            "Product instance created. Name={Name}, CarId={CarId}, Type={Type}",
            name,
            car.Id,
            productType
        );

        Product p = new Product
        {
            CarId = car.Id,
            Name = name,
            Brand = brand,
            Quantity = quantity,
            Price = price,
            ProductType = productType,
            Description = description
        };
        await InsertProduct(p);
        return p.ProductId;
    }

    public async Task RegisterEngine(int productId, int ps, int kw, string displacement, Gear gear, Fuel fuel)
    {
        var p = await _productRepository.GetProductByIdAsync(productId);
        
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
        await _productRepository.UpdateProductAsync(p);
    }

    public async Task RegisterRim(int productId, decimal diameter, decimal width)
    {
        var p = await _productRepository.GetProductByIdAsync(productId);

        p.RimDetail = new RimDetail
        {
            ProductId = p.ProductId,
            DiameterInInch = diameter,
            WidthInInch = width
        };
        _productRepository.UpdateProductAsync(p);
        _logger.LogInformation("Rim registered for product {ProductId}.", p.ProductId);
    }

    public async Task RegisterLights(int productId, int lumen, bool isLED)
    {
        var p = await _productRepository.GetProductByIdAsync(productId);

        p.LightsDetail = new LightsDetail
        {
            ProductId = p.ProductId,
            Lumen = lumen,
            IsLed = isLED
        };
        _productRepository.UpdateProductAsync(p);
        _logger.LogInformation("Light registered for product {ProductId}", p.ProductId);
    }

    public async Task RegisterColor(int productId, string colorName)
    {
        var p = await _productRepository.GetProductByIdAsync(productId);

        p.ColorDetail = new ColorDetail
        {
            ProductId = p.ProductId,
            DisplayName = colorName,
        };
        _productRepository.UpdateProductAsync(p);
        _logger.LogInformation("Color registered for product {ProductId}.", p.ProductId);
    }

    public async Task InsertProduct(Product p)
    {
        await _productRepository.InsertProductAsync(p); // muss SaveChangesAsync machen
        _logger.LogInformation($"Inserted product {p.ProductId}");
        
    }

    public List<ProductDto> GetProducts()
    {
        var products = _productRepository.ListProducts();
        return products.ConvertAll(p => new ProductDto
        {
            ProductId = p.ProductId,
            Name = p.Name,
            Brand = p.Brand,
            Price = p.Price,
            Description = p.Description,
            ArticleNumber = p.ArticleNumber,
            ImageUrl = p.ImageUrl,
            CarId = p.CarId,
            ProductType = p.ProductType,
            Quantity = p.Quantity,
        });
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        return new ProductDto
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Brand = product.Brand,
            Price = product.Price,
            Description = product.Description,
            ArticleNumber = product.ArticleNumber,
            ImageUrl = product.ImageUrl,
            CarId = product.CarId,
            ProductType = product.ProductType,
            Quantity = product.Quantity,
        };
    }

    public async Task DeleteProductAsync(ProductDto p)
    {
        var product = await _productRepository.GetProductByIdAsync(p.ProductId);
        await _productRepository.DeleteProductAsync(product);
        _logger.LogWarning("Product deleted");
    }

    public async Task UpdateProductAsync(ProductDto p)
    {
        var product = await _productRepository.GetProductByIdAsync(p.ProductId);
        product.Name = p.Name;
        product.Brand = p.Brand;
        product.Price = p.Price;
        product.Description = p.Description;
        product.ArticleNumber = p.ArticleNumber;
        product.ImageUrl = p.ImageUrl;
        product.ProductType = p.ProductType;
        product.Quantity = p.Quantity;
        await _productRepository.UpdateProductAsync(product);
        _logger.LogInformation("Product updated");
    }

    public List<ProductType> GetProductTypes()
    {
        return Enum.GetValues(typeof(ProductType)).Cast<ProductType>().ToList();
    }

    public async Task<List<ProductDto>> ProductListQueryAsync(ProductListQuery query)
    {
       var products = await _productRepository.QueryAsync(query);
       return products.ConvertAll(p => new ProductDto
           {
               ProductId = p.ProductId,
               Name = p.Name,
               Brand = p.Brand,
               Price = p.Price,
               Description = p.Description,
               ArticleNumber = p.ArticleNumber,
               ImageUrl = p.ImageUrl,
               CarId = p.CarId,
               ProductType = p.ProductType,
               Quantity = p.Quantity,
           }
       );
    }

    // ___________________________________Cars____________________________________


    public async Task<List<CarDto>> GetCarsAsync()
    {
        
      var cars = await _carRepository.ListCarsAsync();
      return cars.ConvertAll(c => new CarDto
      {
          Brand = c.Brand,
          DatePermit = c.DatePermit,
          DateProduction = c.DateProduction,
          Price = c.Price,
          PS = c.PS,
          Model = c.Model,
          Id = c.Id,
          Name = c.Name,
          Quantity = c.Quantity,
      });
    }

    public async Task<CarDto> RegisterCarAsync(string name, string dateProduction, string datePermit, string brand,
        string model, string ps, string quantity,string price)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(model) || string.IsNullOrWhiteSpace(brand))
            throw new WrongInputException("Name, Hersteller und Modell dürfen nicht leer sein.");
        
        if (string.IsNullOrWhiteSpace(dateProduction) || string.IsNullOrWhiteSpace(datePermit))
            throw new WrongInputException("Zulassungsdatum und Produktionsdatum dürfen nicht leer sein.");
        
        if (!int.TryParse(quantity, out var quantityValue) || quantityValue < 0)
            throw new WrongInputException("Bestand muss eine positive Ganzzahl sein.");
        
        if (!decimal.TryParse(price, out var priceValue) || priceValue < 0)
            throw new WrongInputException("Preis muss eine positive Zahl sein.");

        if (!int.TryParse(ps, out var psValue) || psValue < 0)
            throw new WrongInputException("PS muss eine positive Ganzzahl sein.");

        Car c = new Car();
        c.Name = name;
        c.DateProduction = dateProduction;
        c.DatePermit = datePermit;
        c.Brand = brand;
        c.Model = model;
        c.PS = psValue;
        c.Quantity = quantityValue;
        c.Price = priceValue;
        await _carRepository.InsertCarAsync(c);
        var carDto = new CarDto
        {
            Brand = c.Brand,
            DatePermit = c.DatePermit,
            DateProduction = c.DateProduction,
            Price = c.Price,
            PS = c.PS,
            Model = c.Model,
            Name = c.Name,
            Quantity = c.Quantity,
            Id = c.Id,
        };
        return carDto;
    }

    public async Task<CarDto> GetCarByIdAsync(int id)
    {
        var car = await _carRepository.GetCarByIdAsync(id);
        return car != null ? new CarDto
        {
            Id = car.Id,
            Brand = car.Brand,
            DatePermit = car.DatePermit,
            DateProduction = car.DateProduction,
            Price = car.Price,
            PS = car.PS,
            Model = car.Model,
            Name = car.Name,
            Quantity = car.Quantity,
        } : null;
    }

    public async Task DeleteCarAsync(CarDto car)
    {
        var productsCount = (await _productRepository.GetProductsForCarsAsync(car.Id)).Count;
        if (productsCount > 0){
            throw new ForbiddenActionException("Fahrzeug besitzt "  + productsCount + " registrierte Produkte und darf nicht gelöscht werden.");
        }
        var carToDelete = await _carRepository.GetCarByIdAsync(car.Id);
        await _carRepository.DeleteCarAsync(carToDelete);
        _logger.LogInformation("Car deleted");
    }

    public async Task UpdateCarAsync(CarDto car)
    {
        var carToUpdate = await _carRepository.GetCarByIdAsync(car.Id);
        carToUpdate.Brand = car.Brand;
        carToUpdate.DatePermit = car.DatePermit;
        carToUpdate.DateProduction = car.DateProduction;
        carToUpdate.Price = car.Price;
        carToUpdate.Model = car.Model;
        carToUpdate.Quantity = car.Quantity;
        carToUpdate.PS = car.PS;
        carToUpdate.Name = car.Name;
        
        await _carRepository.UpdateCarAsync(carToUpdate);
        _logger.LogInformation(
            "Car updated. CarId={CarId}, Name={Name}",
            car.Id,
            car.Name
        );
    }

    public async Task<List<ProductDto>> GetAvailableRims(int carId)
    {
        var query = new ProductListQuery
        {
            CarId = carId,
            Type = ProductType.Felge,
        };
        var rims = await _productRepository.QueryAsync(query);
        return rims.ConvertAll(p => new ProductDto
        {
            ProductId = p.ProductId,
            Name = p.Name,
            Brand = p.Brand,
            Price = p.Price,
            Description = p.Description,
            ArticleNumber = p.ArticleNumber,
            ImageUrl = p.ImageUrl,
            CarId = p.CarId,
            Quantity = p.Quantity,
            ProductType = p.ProductType,
        });
    }
    

    public async Task<List<ProductDto>> GetAvailableColors(int carId)
    {
        var query = new ProductListQuery
        {
            CarId = carId,
            Type = ProductType.Lack,
        };
        var colors = await _productRepository.QueryAsync(query);
        return colors.ConvertAll(p => new ProductDto
        {
            ProductId = p.ProductId,
            Name = p.Name,
            Brand = p.Brand,
            Price = p.Price,
            Description = p.Description,
            ArticleNumber = p.ArticleNumber,
            ImageUrl = p.ImageUrl,
            CarId = p.CarId,
            Quantity = p.Quantity,
            ProductType = p.ProductType,
        });
    }

    public async Task<List<CarDto>> CarListQueryAsync(CarListQuery q)
    {
        
      var cars = await _carRepository.QueryAsync(q);
      return cars.ConvertAll(p => new CarDto
      {
          Id = p.Id,
          Brand = p.Brand,
          DatePermit = p.DatePermit,
          DateProduction = p.DateProduction,
          Price = p.Price,
          PS = p.PS,
          Model = p.Model,
          Name = p.Name,
          Quantity = p.Quantity,
      });
    }

    // ___________________________________Orders____________________________________

    public async Task<List<OrderDto>> GetOrdersAsync()
    {
        var orders = await _customerRepository.GetOrdersAsync();
        return orders.ConvertAll(p => new OrderDto
            {
                CustomerId = p.CustomerId,
                OrderId = p.OrderId,
                OrderDate = p.OrderDate,
                TotalPrice = p.TotalPrice,
                DeliveryAddressId = p.DeliveryAddressId,
                PaymentValueId = p.PaymentValueId,
            }
        );
    }

    public async Task<OrderDto> GetOrderByIdAsync(int id)
    {
        var order = await _customerRepository.GetOrderByIdAsync(id);
        return order != null ? new OrderDto
        {
            CustomerId = order.CustomerId,
            OrderId = order.OrderId,
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPrice,
            DeliveryAddressId = order.DeliveryAddressId,
            PaymentValueId = order.PaymentValueId,
        } : null;
    }

    public async Task<List<OrderPositionDto>> GetOrderItemsAsync(int id)
    {
        var orderitems = await _customerRepository.GetOrderItemsAsync(id);
        return orderitems.ConvertAll(p => new OrderPositionDto
            {
                OrderId = p.OrderId,
                OrderPositionId = p.OrderPositionId,
                ArticleNumber = p.ArticleNumber,
                Name = p.Name,
                Brand = p.Brand,
                Type = p.Type,
                UnitPrice = p.UnitPrice,
                Quantity = p.Quantity,
            }
        );
    }
}