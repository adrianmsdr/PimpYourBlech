using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Session;

public class DeliveryAddress
{
    public int Id { get; set; }
    
    public int CustomerId { get; set; }
    
    public Customer Customer { get; set; }
    public string Salutation  { get; set; } = string.Empty;
    public string Surname     { get; set; } = string.Empty;
    public string Lastname    { get; set; } = string.Empty;
    public string Street      { get; set; } = string.Empty;
    public string HouseNumber { get; set; } = string.Empty;
    public string Town        { get; set; } = string.Empty;
    public string PostalCode  { get; set; } = string.Empty;
    public string Country     { get; set; } = string.Empty;
}