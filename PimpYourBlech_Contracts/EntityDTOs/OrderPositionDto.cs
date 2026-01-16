using PimpYourBlech_Contracts.Enums;

namespace PimpYourBlech_Contracts.EntityDTOs;

public class OrderPositionDto
{
    public int OrderPositionId { get; set; }
    public int OrderId { get; set; }
    public string ArticleNumber { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public ProductType Type { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}