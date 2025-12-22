using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Enums;
using PimpYourBlech_ClassLibrary.Inventories;

namespace PimpYourBlech_ClassLibrary.Services.Configurator.Implementation;

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
        var config = new Configuration
        {
            Name = name,
            Car = car,
            CustomerId = customer.Id,
        };
        _customerInventory.AddConfiguration(config);
        customer.Configurations.Add(config);
        return config;
    }
    public List<Configuration> GetAllConfigurationsForCustomer(int customerId)
    {
        return _customerInventory.GetConfigurationsForCustomer(customerId);
    }
    public Car? GetCarById(int carId)
    {
        var c = carInventory.ListCars().FirstOrDefault(c => c.Id == carId);

        if (c == null) return null;

        return c;

    }
    
    public void AddProduct(Configuration configuration, Product product)
    {
        if (product == null || configuration == null)
            return;

        var existingProduct = configuration.Products
            .FirstOrDefault(p => p.ProductType == product.ProductType);

        // exakt gleiches Produkt → nichts tun
        if (existingProduct?.ProductId == product.ProductId)
            return;

        // gleicher ProductType → ersetzen
        if (existingProduct != null)
            configuration.Products.Remove(existingProduct);

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
    public void DeleteConfiguration(Configuration configuration)
    {
            _customerInventory.DeleteConfiguration(configuration);
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

    public List<Product> ListRims()
    {
       return productInventory.ListRims();
    }

    public List<Car> ListCars()
    {
        return carInventory.ListCars();
    }

    public Configuration GetConfigurationById(int Id)
    {
       return _customerInventory.ListConfigurations()
           .FirstOrDefault(c => c.Id == Id);
    }

    public List<Product> GetAvailableColors(int Id)
    {
        return carInventory.GetAvailableColor(Id);
    }

    public Product GetProductById(int Id)
    {
        return productInventory.ListProducts().FirstOrDefault(p => p.ProductId == Id);
    }
}