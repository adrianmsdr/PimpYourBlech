using PimpYourBlech_Contracts.Enums;

namespace PimpYourBlech_Contracts.EntityDTOs;

public class ProductDto
{
    public int ProductId { get; set; }
    
    public int CarId { get; set; }
    // Allgemeine Eigenschaften
    public string Name { get; set; }
    public string ArticleNumber { get; set; }
    public string Brand { get; set; }
    
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public ProductType ProductType { get; set; }
    
    // Motor
    public int? Ps {get; set;}
    public int? Kw {get; set;}
    public Fuel? Fuel {get; set;}
    public Gear Gear {get; set;}
    public string? Displacement {get; set;}
    // Felge
    public decimal? DiameterInInch { get; set; }
    public decimal WidthInInch { get; set; }
    
    public String DisplayName { get; set; }
    
    // Lack
    
    
    
}