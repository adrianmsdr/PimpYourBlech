namespace PimpYourBlech_Contracts.DTOs;

public class CustomerOrderCheckDto
{
    public int CustomerId { get; set; }
    public bool HasOrder { get; set; }
    public bool HasAddress { get; set; }
    public bool HasPaymentValue { get; set; }
}