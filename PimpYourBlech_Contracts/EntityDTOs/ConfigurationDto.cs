namespace PimpYourBlech_Contracts.EntityDTOs;

public class ConfigurationDto
{
    public int Id { get; set; }
    
    public string? Name {get; set;}

    // FK zu Customer
    public int CustomerId { get; set; }
   
    // FK zu Car
    public int CarId { get; set; }
    
    
    public decimal TotalPrice { get; set; }
    
    public int ProductCount   { get; set; }

    public int TotalPs { get; set; }
    
    public CarDto Car { get; set; } = new();
    
    public override string ToString()
    {
        return $"Name: {Name}";
    }
}