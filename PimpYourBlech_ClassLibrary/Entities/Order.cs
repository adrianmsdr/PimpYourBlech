using PimpYourBlech_ClassLibrary.Session;

namespace PimpYourBlech_ClassLibrary.Entities;

public class Order 
{
    public int OrderId { get; set; }
    public Customer Customer { get; set; }
    
    public int CustomerId { get; set; }
    public Product Product { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public DeliveryAddress DeliveryAddress { get; set; }
    public Car? Car { get; set; }
}