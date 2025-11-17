using TestAutoKonfigurator.Inventories.InventoryService;

namespace TestAutoKonfigurator.Configorator.Implementation;

public class ConfiguratorService(ICustomerService service):IConfiguratorService
{

    private readonly ICustomerService service = service;
    public Configuration StartNewConfiguration(Customer customer, Car car,string name)
    {
        Car copy = new Car(car.Name,car.DateProduction,car.DatePermit,car.Brand,car.Model,car.PS,car.Quantity,car.Price);
        Configuration config = new Configuration(copy, name);
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
        service.UpdateCustomers();
    }
}