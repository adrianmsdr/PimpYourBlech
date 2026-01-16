

namespace PimpYourBlech_Data.Models;

public class Order 
{
    public int OrderId { get; set; }
    public Customer Customer { get; set; } = null!;
    
    public int CustomerId { get; set; }
    public List<OrderPosition> Items { get; set; } = new();
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public int DeliveryAddressId { get; set; }
    public DeliveryAddress DeliveryAddress { get; set; } = null!;
    
    public int PaymentValueId { get; set; }

    public PaymentValue PaymentValue { get; set; } = null!;
}