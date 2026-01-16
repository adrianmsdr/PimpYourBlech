namespace PimpYourBlech_Contracts.DTOs;

public class OrderCustomerDto
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime Created { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    
}