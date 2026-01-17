namespace PimpYourBlech_Contracts.EntityDTOs;

public class RimDetailDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }

    public decimal DiameterInInch { get; set; }
    public decimal WidthInInch { get; set; }
}