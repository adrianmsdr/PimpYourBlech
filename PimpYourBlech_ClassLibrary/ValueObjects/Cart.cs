
using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.ValueObjects;

public class Cart
{
    public List<CartPosition> Products { get; set; } = new();
    
    public CustomerDto Customer { get; set; }
}