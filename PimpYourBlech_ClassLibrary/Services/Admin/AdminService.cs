using System.Security.Cryptography;
using System.Text;
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
   
    public AdminService(ICustomerInventory costumers, IProductInventory products, ICarInventory car,  IEmailService email)
    {
        _customerRepository  = costumers;
        _productRepository = products;
        _carRepository = car;
        _emailService = email;
    }

    public List<Customer> GetListCustomers()
    {
        return _customerRepository.ListCustomers();
    }
    
    
    public async Task RegisterCustomerAsync(string firstName, string lastName, string username, string passwordHash, string passwordHashConfirm, string telefon,
        string mailAddress, string mailAddressConfirm,string ImagePath)
    {
        // Username darf nicht leer sein
        if (string.IsNullOrWhiteSpace(username))
            throw new WrongInputException("Username darf nicht leer sein.");

        // Email darf nicht leer sein
        if (string.IsNullOrWhiteSpace(mailAddress))
            throw new WrongInputException("Email darf nicht leer sein.");

        // Email Validierung
        if(!_emailService.IsValid(mailAddress))
            throw new WrongInputException("Keine gültige Email Adresse. (Beispiel: user@example.com)");

        _emailService.ConfirmRegistrationChecker(mailAddress, mailAddressConfirm);

        // Verfügbarkeit des Usernames checken
        isUsernameAvailable(username);

        // Passwörter auf Übereinstimmung checken
        if (passwordHash != passwordHashConfirm)
            throw new WrongPasswordException("Die Passwörter stimmen nicht überein.");

        var hash = Convert.ToBase64String(
            SHA256.HashData(Encoding.UTF8.GetBytes(passwordHash ?? ""))
        );
        Customer customer = new Customer();
        customer.FirstName = firstName;
        customer.LastName = lastName;
        customer.Username = username;
        customer.PasswordHash = hash;
        customer.Telefon = telefon;
        customer.MailAddress = mailAddress;
        customer.ImagePath = ImagePath;
        _customerRepository.InsertCustomer(customer);
        _emailService.SendRegistrationEmail(customer);
    }
    
    public Customer Login(string username, string passwordHash)
    {
        throw new NotImplementedException();
    }

    public bool isUsernameAvailable(string username)
    {
        bool usernameAccepted = true;

            if (username.Length < 8)
            {
                throw new UsernameNotAvailableException("Username zu kurz. Mindestens 8 Zeichen.");
            }

            foreach(Customer customer in _customerRepository.ListCustomers())
            if (customer.Username == username)
            {
                throw new UsernameNotAvailableException("Username ist bereits vergeben, bitte wähle einen anderen!");
            }
            return usernameAccepted;
        
    }
    
    public bool LoginAccepted(string username, string passwordHash)
    {
        bool login = false;
        foreach (Customer c in _customerRepository.ListCustomers())
        { 
            if (c.Username == username && c.PasswordHash == passwordHash)
            {
                return true;
            }

        }

        return login;
    }

    public Customer GetCustomer(string username, string passwordHash)
    {
        Customer temp = null;
        foreach (Customer c in _customerRepository.ListCustomers())
        {
            if (c.Username == username && c.PasswordHash == passwordHash)
            {
                temp = c;
            }

        }
        return temp;
    }

    public void DeleteAllCustomers()
    {
        _customerRepository.DeleteCustomers();
    }
    
    public Customer GetCustomerByUsername(String username)
    {
        Customer temp = null;
        foreach (Customer c in _customerRepository.ListCustomers())
        {
            if (c.Username == username)
            {
                temp = c;
            }
        }

        if (temp == null)
        {
            throw new NoCustomerFoundException("Kein Benutzer mit diesem Username gefunden.");
        }
        return temp;
    }

   

    public Customer GetCustomerByTelefon(String telefon)
    {
        Customer temp = null;
        foreach (Customer c in _customerRepository.ListCustomers())
        {
            if (c.Telefon == telefon)
            {
                temp = c;
            }
        }
        
        if (temp == null)
        {
            throw new NoCustomerFoundException("Kein Benutzer mit dieser Telefonnummer gefunden.");
        }

        return temp;
    }
    
    public Customer GetCustomerByNames(String firstName, String lastName)
    {
        Customer temp = null;
        foreach (Customer c in _customerRepository.ListCustomers())
        {
            if (c.FirstName== firstName&&c.LastName==lastName)
            {
                temp = c;
            }
            
            if (temp == null)
            {
                throw new NoCustomerFoundException("Kein Benutzer mit diesen Namen gefunden.");
            }
        }

        return temp;
    }
    
    public void UpdateCustomer(Customer c)
    {
        _customerRepository.UpdateCustomer(c);
    }
    
    public void DeleteCustomer(Customer c)
    {
        _customerRepository.DeleteCustomer(c);
    }
    
    public void UpdateCustomers()
    {
        _customerRepository.UpdateCustomers();

    }
    
    public Customer GetCustomerById(int id)
    {
        Customer temp = null;
        foreach (Customer c in _customerRepository.ListCustomers())
        {
            if (c.Id == id)
            {
                temp = c;
            }
        }
        return temp;
    }
    
    
    // ___________________________________Poducts____________________________________
    public Product CreateProduct(Car car, string name, string brand, int quantity, double price, ProductType productType)
    {
        Product temp  = new Product();
        temp.Car = car;
        temp.CarId = car.Id;
        temp.Name = name;
        temp.Brand = brand;
        temp.Quantity = quantity;
        temp.Price = price;
        temp.ProductType = productType;
        
        
        return temp;
    }
    
    public async Task<Product> InsertProduct(Car car,Product p)
    {

        await _productRepository.InsertProduct(p);
        return p;
    }

    public async Task<Product>RegisterEngine(Product p, int ps, int kw, string displacement, Gear gear, Fuel fuel)
    {
        EngineDetail temp = new EngineDetail();
        temp.Product = p;
        temp.Ps = ps;
        temp.Kw = kw;
        temp.Displacement = displacement;
        temp.Gear = gear;
        temp.Fuel = fuel;
        p.EngineDetail =  temp;
        await _productRepository.InsertProduct(p);
        return p;
    }

    public Product RegisterRim(Product p, decimal diameter, decimal width)
    {
        RimDetail temp = new RimDetail();
        temp.Product = p;
        temp.DiameterInInch = diameter;
        temp.WidthInInch = width;
        p.RimDetail = temp;
        _productRepository.InsertProduct(p);
        
        return p;
    }

    public Product RegisterLights(Product p, int lumen, bool isLED)
    {
        LightsDetail temp = new LightsDetail();
        temp.Product = p;
        temp.Lumen = lumen;
        temp.IsLed = isLED;
        p.LightsDetail = temp;
        _productRepository.InsertProduct(p);
        return p;
    }

    public Product RegisterColor(Product p, Car c,string colorName)
    {
        ColorDetail temp = new ColorDetail();
        temp.ProductId = p.ProductId;
        temp.Product = p;
        temp.DisplayName = colorName;
        p.ColorDetail = temp;
        _productRepository.InsertProduct(p);
        c.Colors.Add(p);

        return p;
    }

    public List<Product> GetProducts()
    {
        return _productRepository.ListProducts();
    }
    
    public Product GetProductById(int id)
    {
        Product temp = null;
        foreach (Product p in _productRepository.ListProducts())
        {
            if (p.ProductId == id)
            {
                temp = p;
            }
        }
        return temp;
    }

    public void DeleteProduct(Product p)
    {
        _productRepository.DeleteProduct(p);

    }

    public void UpdateProduct(Product p)
    {
        _productRepository.UpdateProduct(p);
    }
    
    public List<ProductType> GetProductTypes()
    {
        return Enum.GetValues(typeof(ProductType)).Cast<ProductType>().ToList();
    }

    // ___________________________________Cars____________________________________


    public List<Car> GetCars()
    {
        return _carRepository.ListCars();
    }
    public Car RegisterCar(string name, string dateProduction, string datePermit, string brand, string model, int ps, int quantity,
        double price)
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
        _carRepository.InsertCar(c);
        return c;
    }
    
    public Car GetCarById(int id)
    {
        Car temp = null;
        foreach (Car c in _carRepository.ListCars())
        {
            if (c.Id == id)
            {
                temp = c;
            }
        }
        return temp;
    }

    public void DeleteCar(Car car)
    {
        _carRepository.DeleteCar(car);
    }

    public void UpdateCar(Car car)
    {
        _carRepository.UpdateCar(car);
    }
    
    public List<Product> GetAvailableRims(int carId)
    {
        return GetProducts()
            .Where(p => p.CarId == carId && p.ProductType == ProductType.Rim)
            .ToList();
    }

    public List<Product> GetAvailableColors(int carId)
    {
        return GetProducts()
            .Where(p => p.CarId == carId && p.ProductType == ProductType.Color)
            .ToList();
    }
}