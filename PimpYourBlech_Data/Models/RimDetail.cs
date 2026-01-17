namespace PimpYourBlech_Data.Models;

public class RimDetail
{
    
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public decimal DiameterInInch { get; set; }
    public decimal WidthInInch { get; set; }
}