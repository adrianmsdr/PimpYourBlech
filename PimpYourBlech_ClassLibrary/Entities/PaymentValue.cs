namespace PimpYourBlech_ClassLibrary.Entities;

public class PaymentValue
{
    public int Id { get; set; }
    
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public string AccountOwner { get; set; } = string.Empty;
    public string Iban         { get; set; } = string.Empty;
    public string Bic          { get; set; } = string.Empty;
}