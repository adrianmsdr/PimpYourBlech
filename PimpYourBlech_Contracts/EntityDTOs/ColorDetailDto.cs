namespace PimpYourBlech_Contracts.EntityDTOs;

public class ColorDetailDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }

    public ProductDto Product { get; set; }
    
    public String DisplayName { get; set; }
}