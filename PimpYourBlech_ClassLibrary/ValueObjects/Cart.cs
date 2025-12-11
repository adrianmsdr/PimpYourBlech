using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.ValueObjects;

public class Cart
{
    public List<Product> Products { get; set; }
    public Customer Customer { get; set; }
}