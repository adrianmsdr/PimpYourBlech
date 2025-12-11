using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.ValueObjects;

public class Cart
{
    public List<Product> Products { get; set; } = new List<Product>();
    public Customer Customer { get; set; }
}