namespace TestAutoKonfigurator;

public class Configuration(Car car, string name)
{
    private readonly List<Product> _products;
    
    public string Name {get; set;} = name;

    public Car Car { get; set; } = car;
    public Engine Engine {get; set;}

    public override string ToString()
    {
       return  "Auto: " + Car.Name; 
    }
}