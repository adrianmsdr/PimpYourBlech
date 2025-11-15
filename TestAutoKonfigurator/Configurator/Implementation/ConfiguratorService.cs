namespace TestAutoKonfigurator.Configorator.Implementation;

public class ConfiguratorService:IConfiguratorService
{
    public Configuration StartNewConfiguration(Customer customer, Car car,string name)
    {
        Car copy = new Car(car.Name,car.DateProduction,car.DatePermit,car.Brand,car.Model,car.PS,car.Quantity,car.Price);
        Configuration config = new Configuration(copy, name);
        customer.Configurations.Add(config);
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
        foreach (var config in customer.Configurations)
        {
            if (config == configuration)
            {
                customer.Configurations.Remove(configuration);
            }
        }
    }
}