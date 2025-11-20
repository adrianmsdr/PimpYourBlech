using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Inventories;

namespace PimpYourBlech_ClassLibrary.Services.Configurator.Implementation;

public class ConfiguratorService(ICustomerInventory customers, IProductInventory products, ICarInventory cars):IConfiguratorService
{

    private readonly ICustomerInventory _customerInventory = customers;
    private readonly IProductInventory productInventory = products;
    private readonly ICarInventory carInventory = cars;
    public Configuration StartNewConfiguration(Customer customer, Car car,string name)
    {
        Car copy = new Car();
        copy.Name = car.Name;
        copy.DateProduction = car.DateProduction;
        copy.DatePermit = car.DatePermit;
        copy.Brand = car.Brand;
        copy.Model = car.Model;
        copy.PS = car.PS;
        copy.Quantity = car.Quantity;
        copy.Price = car.Price;
        Configuration config = new Configuration();
        config.Name = name;
        config.Car = car;
        customer.Configurations.Add(config);
        SaveConfigurations();
        return config;
    }

    public void AddProduct(Configuration configuration, Product product)
    {
        throw new NotImplementedException();
    }

    public void RemoveProduct(Configuration configuration, Product product)
    {
        throw new NotImplementedException();
    }

    public double CalculateTotalPrice(Configuration configuration)
    {
        throw new NotImplementedException();
    }

    public void SaveConfiguration(Configuration configuration)
    {
        throw new NotImplementedException();
    }

    public void DeleteConfiguration(Configuration configuration, Customer customer)
    {
            customer.Configurations.Remove(configuration);
        
    }

    public List<Configuration> GetAllConfigurations(Customer customer)
    {
        return customer.Configurations;
    }

    public void SaveConfiguration(Configuration configuration, Customer customer)
    {
        throw new NotImplementedException();
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