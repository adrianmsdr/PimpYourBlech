using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.ValueObjects;

public class CartPosition
{
    
    public int CustomerId { get; set; }
    public int ProductId { get; set; }

    public Product Product { get; set; }
    public decimal Price { get; set; } = 0;
    public int Quantity { get; set; }
}