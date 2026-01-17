using PimpYourBlech_Contracts.Enums;

namespace PimpYourBlech_Data.Models;

public class OrderPosition
{
    public int OrderPositionId { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
public string ArticleNumber { get; set; }
public string Name { get; set; }
public string Brand { get; set; }
public ProductType Type { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}