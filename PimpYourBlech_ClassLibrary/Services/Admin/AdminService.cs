using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Enums;
using PimpYourBlech_ClassLibrary.Exceptions;
using PimpYourBlech_ClassLibrary.Inventories;

namespace PimpYourBlech_ClassLibrary.Services.Admin;

public class AdminService:IAdminService
{
    
    private readonly ICustomerInventory customerRepository;
    private readonly IProductInventory productRepository;
    private readonly ICarInventory carRepository;
   
    public AdminService(ICustomerInventory costumers, IProductInventory products, ICarInventory car)
    {
        customerRepository  = costumers;
        productRepository = products;
        carRepository = car;
    }

    public List<Customer> GetListCustomers()
    {
        return customerRepository.ListCustomers();
    }
    
    
    public Customer Register(string firstName, string lastName, string username, string passwordHash, string telefon,
        string mailAddress)
    {
        Customer customer = new Customer();
        customer.FirstName = firstName;
        customer.LastName = lastName;
        customer.Username = username;
        customer.PasswordHash = passwordHash;
        customer.Telefon = telefon;
        customer.MailAddress = mailAddress;
        customerRepository.InsertCustomer(customer);
        return customer;
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
                throw new UsernameNotAvailableException("Username zu kurz. Mindestens 8 Zeichen.1");
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
    
    public void UpdateCustomer(Customer c,String username, String passwordHash, String telefon)
    {
        customerRepository.UpdateCustomer(c, username, passwordHash, telefon);
    }
    
    public void DeleteCustomer(Customer c)
    {
        customerRepository.DeleteCustomer(c);
    }
    
    public void UpdateCustomers()
    {
        customerRepository.UpdateCustomers();

    }

    
    // ___________________________________Poducts____________________________________
    public Product RegisterProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public List<Product> GetProducts()
    {
        return productRepository.ListProducts();
    }

    

    public Engine RegisterEngine(string name,
        string articleNumber,
        string brand,
        string description,
        int quantity,
        double price,
        int _ps,
        int _kw,
        string _displacement,
        Gear _gear)
    {
        Engine engine = new Engine();
        engine.Name = name;
        engine.ArticleNumber = articleNumber;
        engine.Brand = brand;
        engine.Description = description;
        engine.Quantity = quantity;
        engine.Price = price;
        engine.Ps = _ps;
        engine.Kw = _kw;
        engine.Displacement = _displacement;
        engine.Gear = _gear;
        productRepository.InsertEngine(engine);
        
        return engine;
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
}