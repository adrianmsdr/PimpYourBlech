namespace PimpYourBlech_ClassLibrary.Entities;

public class Order 
{
    public int OrderId { get; set; }
    public Customer Customer { get; set; }
    public Product Product { get; set; }
    public Car? Car { get; set; }
}