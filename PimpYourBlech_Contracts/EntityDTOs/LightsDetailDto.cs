namespace PimpYourBlech_Contracts.EntityDTOs;

public class LightsDetailDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Lumen { get; set; }
    public bool IsLed { get; set; }
}