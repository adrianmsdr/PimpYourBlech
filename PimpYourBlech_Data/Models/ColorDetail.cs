namespace PimpYourBlech_Data.Models;

public class ColorDetail
{
    public int Id { get; set; }
    public int ProductId { get; set; }

    public Product Product { get; set; }
    
    public String DisplayName { get; set; }
}