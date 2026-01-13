using PimpYourBlech_ClassLibrary.Enums;

namespace PimpYourBlech_ClassLibrary.Entities;

public class Product
{
    public int ProductId { get; set; }
    
    public int CarId { get; set; }
    
    public Car Car { get; set; }
// Allgemeine Eigenschaften
    public string Name { get; set; }
    public string ArticleNumber { get; set; }
    public string Brand { get; set; }
    
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public ProductType ProductType { get; set; }
    
    // Many-to-Many
    public List<Configuration> Configurations { get; set; } = new List<Configuration>();
    
    public EngineDetail?  EngineDetail { get; set; }
    public RimDetail?  RimDetail { get; set; }
    public LightsDetail? LightsDetail { get; set; }
    
    public ColorDetail? ColorDetail { get; set; }
}