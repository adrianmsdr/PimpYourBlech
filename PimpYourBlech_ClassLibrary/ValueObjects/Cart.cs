using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.ValueObjects;

public class Cart
{
    public List<CartPosition> Products { get; set; } = new();
    public Customer Customer { get; set; }
}