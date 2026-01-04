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
    private readonly ICustomerInventory customerRepository;
    private readonly IProductInventory productRepository;
    private readonly ICarInventory carRepository;
    private readonly IEmailService emailService;
   
    public AdminService(ICustomerInventory costumers, IProductInventory products, ICarInventory car,  IEmailService email)
    {
        customerRepository  = costumers;
        productRepository = products;
        carRepository = car;
        emailService = email;
    }

    public List<Customer> GetListCustomers()
    {
        return customerRepository.ListCustomers();
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
        if(!emailService.IsValid(mailAddress))
            throw new WrongInputException("Keine gültige Email Adresse. (Beispiel: user@example.com)");

        emailService.ConfirmRegistrationChecker(mailAddress, mailAddressConfirm);

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
        customerRepository.InsertCustomer(customer);
        emailService.SendRegistrationEmail(customer);
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

            foreach(Customer customer in customerRepository.ListCustomers())
            if (customer.Username == username)
            {
                throw new UsernameNotAvailableException("Username ist bereits vergeben, bitte wähle einen anderen!");
            }
            return usernameAccepted;
        
    }
    
    public bool LoginAccepted(string username, string passwordHash)
    {
        bool login = false;
        foreach (Customer c in customerRepository.ListCustomers())
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
        foreach (Customer c in customerRepository.ListCustomers())
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
        customerRepository.DeleteCustomers();
    }
    
    public Customer GetCustomerByUsername(String username)
    {
        Customer temp = null;
        foreach (Customer c in customerRepository.ListCustomers())
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
        foreach (Customer c in customerRepository.ListCustomers())
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
        foreach (Customer c in customerRepository.ListCustomers())
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
        customerRepository.UpdateCustomer(c);
    }
    
    public void DeleteCustomer(Customer c)
    {
        customerRepository.DeleteCustomer(c);
    }
    
    public void UpdateCustomers()
    {
        customerRepository.UpdateCustomers();

    }
    
    public Customer GetCustomerById(int id)
    {
        Customer temp = null;
        foreach (Customer c in customerRepository.ListCustomers())
        {
            if (c.Id == id)
            {
                temp = c;
            }
        }
        return temp;
    }
    
    
    // ___________________________________Poducts____________________________________
    public Product CreateProduct(Car car, string name, string articleNumber, string brand, int quantity, double price, ProductType productType)
    {
        Product temp  = new Product();
        temp.Car = car;
        temp.CarId = car.Id;
        temp.Name = name;
        temp.ArticleNumber = articleNumber;
        temp.Brand = brand;
        temp.Quantity = quantity;
        temp.Price = price;
        temp.ProductType = productType;
        
        
        return temp;
    }

    public Product RegisterEngine(Product p, int ps, int kw, string displacement, Gear gear)
    {
        EngineDetail temp = new EngineDetail();
        temp.Product = p;
        temp.Ps = ps;
        temp.Kw = kw;
        temp.Displacement = displacement;
        temp.Gear = gear;
        p.EngineDetail =  temp;
        productRepository.InsertProduct(p);
        
        return p;
    }

    public Product RegisterRim(Product p, decimal diameter, decimal width)
    {
        RimDetail temp = new RimDetail();
        temp.Product = p;
        temp.DiameterInInch = diameter;
        temp.WidthInInch = width;
        p.RimDetail = temp;
        productRepository.InsertProduct(p);
        
        return p;
    }

    public Product RegisterLights(Product p, int lumen, bool isLED)
    {
        LightsDetail temp = new LightsDetail();
        temp.Product = p;
        temp.Lumen = lumen;
        temp.IsLed = isLED;
        p.LightsDetail = temp;
        productRepository.InsertProduct(p);
        return p;
    }

    public Product RegisterColor(Product p, Car c,string colorName)
    {
        ColorDetail temp = new ColorDetail();
        temp.ProductId = p.ProductId;
        temp.Product = p;
        temp.DisplayName = colorName;
        p.ColorDetail = temp;
        productRepository.InsertProduct(p);
        c.Colors.Add(p);

        return p;
    }

    public List<Product> GetProducts()
    {
        return productRepository.ListProducts();
    }

    


    public Product GetProductById(int id)
    {
        Product temp = null;
        foreach (Product p in productRepository.ListProducts())
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
        productRepository.DeleteProduct(p);

    }

    public void UpdateProduct(Product p)
    {
        productRepository.UpdateProduct(p);
    }

    // ___________________________________Cars____________________________________


    public List<Car> GetCars()
    {
        return carRepository.ListCars();
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
        carRepository.InsertCar(c);
        return c;
    }
    
    public Car GetCarById(int id)
    {
        Car temp = null;
        foreach (Car c in carRepository.ListCars())
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
        carRepository.DeleteCar(car);
    }

    public void UpdateCar(Car car)
    {
        carRepository.UpdateCar(car);
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