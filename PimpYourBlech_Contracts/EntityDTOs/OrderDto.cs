namespace PimpYourBlech_Contracts.EntityDTOs;

public class OrderDto
{
    public int OrderId { get; set; }
    
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public int DeliveryAddressId { get; set; }
    
    public int PaymentValueId { get; set; }

}