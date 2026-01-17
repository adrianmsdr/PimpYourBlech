namespace PimpYourBlech_Contracts.EntityDTOs;

public class PaymentValueDto
{
    public int Id { get; set; }
    
    public int CustomerId { get; set; }

    public string AccountOwner { get; set; } = string.Empty;
    public string Iban         { get; set; } = string.Empty;
    public string Bic          { get; set; } = string.Empty;
}