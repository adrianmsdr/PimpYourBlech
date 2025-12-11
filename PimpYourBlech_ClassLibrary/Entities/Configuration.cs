namespace PimpYourBlech_ClassLibrary.Entities;

public class Configuration()
{
    public int Id { get; set; }
    
    //private readonly List<Product>? _products;
    
    public string? Name {get; set;}

    public Car? Car { get; set; }
    //public Engine Engine {get; set;}
    
    //Liste von produkten als öffentliche property
    public List<Product> Products { get; set; } = new List<Product>();

    public override string ToString()
    {
        return $"Name: {Name}";
    }
}