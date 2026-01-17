using PimpYourBlech_Contracts.Enums;

namespace PimpYourBlech_Data.Models;

public class EngineDetail{

    public int Id { get; set; }          // Primärschlüssel (PK)

    public int ProductId { get; set; }   // FK zu Product
    public Product Product { get; set; }
    
    public Fuel Fuel { get; set; }
    
    public int Ps { get; set; } 
    public int Kw { get; set; } 
    
    public string Displacement{get;set;} 
    
    public Gear Gear { get; set; }
}