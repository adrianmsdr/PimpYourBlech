namespace TestAutoKonfigurator;

public class Configuration()
{
    public int Id { get; set; }
    
    private readonly List<Product> _products;
    
    public string Name {get; set;}

    public Car Car { get; set; }
    //public Engine Engine {get; set;}

    public override string ToString()
    {
        return "Name: " + Name;
    }
}