using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Inventories;
using PimpYourBlech_ClassLibrary.Inventories.Implementation;

namespace PimpYourBlech_ClassLibrary.Services.Configurator.Implementation;

//public class ConfiguratorService(ICustomerInventory customers, IProductInventory products, ICarInventory cars):IConfiguratorService
public class ConfiguratorService : IConfiguratorService
{

    private readonly ICustomerInventory _customerInventory;
    private readonly IProductInventory productInventory;
    private readonly ICarInventory carInventory;

    public ConfiguratorService(
        ICustomerInventory customers,
        IProductInventory products,
        ICarInventory cars
    )
    {
        _customerInventory = customers;
        productInventory = products;
        carInventory = cars;
    }
    public Configuration StartNewConfiguration(Customer customer, Car car,string name)
    {
        
        //Auto kopieren
        var copy = new Car()
        {
            Id = car.Id,
            Name = car.Name,
            DateProduction = car.DateProduction,
            DatePermit = car.DatePermit,
            Brand = car.Brand,
            Model = car.Model,
            PS = car.PS,
            Quantity = car.Quantity,
            Price = car.Price,
        };
            
            var config = new Configuration
        {
            Name = name,
            Car = copy,
        };
        customer.Configurations.Add(config);
        SaveConfigurations();
        
        return config;
    }

    public Car? GetCarById(int carId)
    {
        var c = carInventory.ListCars().FirstOrDefault(c => c.Id == carId);

        if (c == null) return null;

        return new Car
        {
            Id = c.Id,
            Name = c.Name,
            DateProduction = c.DateProduction,
            DatePermit = c.DatePermit,
            Brand = c.Brand,
            Model = c.Model,
            PS = c.PS,
            Quantity = c.Quantity,
            Price = c.Price,
        };
    }
    
    public void AddProduct(Configuration configuration, Product product)
    {
        configuration.Products.Add(product);
        SaveConfigurations();
    }
    
    public void RemoveProduct(Configuration configuration, Product product)
    {
        configuration.Products.Remove(product);
        SaveConfigurations();
    }

    //Preisberechnung
    public double CalculateTotalPrice(Configuration configuration)
    {
        double carPrice = configuration.Car?.Price ?? 0;
        double productTotal = configuration.Products.Sum(p => p.Price);
        
        return carPrice + productTotal;
    }

    //Config Speicher
    public void SaveConfiguration(Configuration configuration)
    {
        SaveConfigurations();
    }

    public void SaveConfiguration(Configuration configuration, Customer customer)
    {
        if (!customer.Configurations.Contains(configuration))
            customer.Configurations.Add(configuration);
        
        SaveConfigurations();
    }
    public void DeleteConfiguration(Configuration configuration, Customer customer)
    {
            customer.Configurations.Remove(configuration);
            SaveConfigurations();
        
    }

    public List<Configuration> GetAllConfigurations(Customer customer)
    {
        return customer.Configurations;
    }

   

    public void SaveConfigurations()
    {
        _customerInventory.UpdateCustomers();
    }

    public List<Product> ListEngines()
    {
        return productInventory.ListEngines();
    }

    public List<Car> ListCars()
    {
        return carInventory.ListCars();
    }
    
}