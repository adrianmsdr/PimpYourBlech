using System.Text.Json;

namespace TestAutoKonfigurator.Database.Implementation;

public class JsonDatabase : IJsonDatabase
{
    private const string FileUrlCustomers = "customers.json"; 
    private const string FileUrlProducts = "products.json";
    private const string FileUrlCars = "cars.json";

    public List<Customer> LoadCustomers()
    {
        if (!File.Exists(FileUrlCustomers))
        {
            return new List<Customer>();
        }
        
        var json = File.ReadAllText(FileUrlCustomers);
        var customers = JsonSerializer.Deserialize<List<Customer>>(json);
        return customers;
    }
    

    public List<Product> LoadProducts()
    {
        if (!File.Exists(FileUrlProducts))
        {
            return new List<Product>();
        }
        
        var json = File.ReadAllText(FileUrlProducts);
        var products = JsonSerializer.Deserialize<List<Product>>(json);
        return products;
    }

    public List<Car> LoadCars()
    {
        if (!File.Exists(FileUrlCars))
        {
            return new List<Car>();
        }
        
        var json = File.ReadAllText(FileUrlCars);
        var cars = JsonSerializer.Deserialize<List<Car>>(json);
        return cars;
    }

    public void SaveCustomers(List<Customer> customers)
    {
        var json = JsonSerializer.Serialize(customers, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FileUrlCustomers, json);
    }

    

   


    public void SaveProducts(List<Product> products)
    {
        var json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FileUrlProducts, json);
    }

    public void SaveCars(List<Car> cars)
    {
        var json = JsonSerializer.Serialize(cars, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FileUrlCars, json);
    }
}