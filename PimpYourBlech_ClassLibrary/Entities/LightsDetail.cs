namespace PimpYourBlech_ClassLibrary.Entities;

public class LightsDetail
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Lumen { get; set; }
    public bool IsLed { get; set; }
}