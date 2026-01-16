namespace PimpYourBlech_Contracts.EntityDTOs;

public class ConfigurationDto
{
    public int Id { get; set; }
    
    public string? Name {get; set;}

    // FK zu Customer
    public int CustomerId { get; set; }
   
    // FK zu Car
    public int CarId { get; set; }
    
    public override string ToString()
    {
        return $"Name: {Name}";
    }
}