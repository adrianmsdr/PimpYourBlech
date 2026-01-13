using System.ComponentModel.DataAnnotations;
using System.Reflection;
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
    public async Task<Car> GetCarByIdAsync(int carId)
    {
        return await carInventory.GetCarByIdAsync(carId);
        

    }
    
    public async Task AddProduct(int configurationId, int productId)
    {
        if (productId == 0||configurationId == 0)
            return;
        var configuration = GetConfigurationById(configurationId);
        if (configuration == null)
            return;
        
        var product = GetProductById(productId);
        
        var existingProduct = configuration.Products
            .FirstOrDefault(p => p.ProductType == product.ProductType);

        if (product.ProductType == ProductType.Lack
            || product.ProductType == ProductType.Felge
            || product.ProductType == ProductType.Motor)
        {
            if (existingProduct?.ProductId == product.ProductId)
                return;
            if (existingProduct != null)
                configuration.Products.Remove(existingProduct);
        }
        else
        {
            if (existingProduct?.ProductId == product.ProductId)
            {
                configuration.Products.Remove(existingProduct);
                return;
            }
        }
        configuration.Products.Add(product);
        SaveConfigurations();
    
    }

    //Preisberechnung
    public decimal CalculateTotalPrice(Configuration configuration)
    {
        decimal carPrice = configuration.Car?.Price ?? 0;
        decimal productTotal = configuration.Products.Sum(p => p.Price);
        
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

    public async Task<List<Car>> ListCarsAsync()
    {
        return await carInventory.ListCarsAsync();
    }

    public Configuration GetConfigurationById(int Id)
    {
       return _customerInventory.ListConfigurations()
           .FirstOrDefault(c => c.Id == Id);
    }

    public async Task<List<Product>> GetAvailableColorsAsync(int Id)
    {
        return await carInventory.GetAvailableColorsAsync(Id);
    }

    public async Task<List<Product>> GetAvailableEnginesAsync(int carId)
    {
        return ListEngines()
            .Where(p => p.CarId == carId && p.ProductType == ProductType.Motor)
            .ToList();
    }

    public List<Product> GetAvailableRims(int carId)
    {
        return ListRims()
            .Where(p => p.CarId == carId && p.ProductType == ProductType.Felge)
            .ToList();
    }
    public Product GetProductById(int Id)
    {
        return productInventory.ListProducts().FirstOrDefault(p => p.ProductId == Id);
    }
    
    public string GetGearDisplayName(Gear gear)
    {
        var field = gear.GetType().GetField(gear.ToString());
        return field?
            .GetCustomAttribute<DisplayAttribute>()?
            .Name ?? gear.ToString();
    }

    public List<Product> GetAvailableExtras(int carId)
    {
        return productInventory.ListProducts().Where(p => p.CarId == carId)
            .Where(p=>p.ProductType!=ProductType.Lack&&p.ProductType!=ProductType.Motor&&p.ProductType!=ProductType.Felge).ToList();
    }

    public async Task<List<Product>> GetAvailableProductsAsync(int carId, ProductType type)
    {
        return await carInventory.GetAvailableProductsAsync(carId, type);
    }

    public async Task<bool> ConfigurationAvailable(int carId)
    {
        return !(await carInventory.GetAvailableProductsAsync(carId, ProductType.Lack)).Any() ||
               !(await carInventory.GetAvailableProductsAsync(carId, ProductType.Motor)).Any() ||
               !(await carInventory.GetAvailableProductsAsync(carId, ProductType.Felge)).Any()
               ;
    }

    public async Task<List<Car>> GetAvailableCarsAsync()
    {
        return await carInventory.ListCarsForConfigurationsAsync();
    }
}