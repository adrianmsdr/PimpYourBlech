namespace PimpYourBlech_ClassLibrary.Entities;

public class Configuration()
{
    public int Id { get; set; }
    
    public string? Name {get; set;}

    // FK + Navigation zu Customer
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
   
    // FK + Navigation zu Car
    public int CarId { get; set; }
    public Car Car { get; set; } = null!;
    public List<Product> Products { get; set; } = new List<Product>();

    public override string ToString()
    {
        return $"Name: {Name}";
    }
}