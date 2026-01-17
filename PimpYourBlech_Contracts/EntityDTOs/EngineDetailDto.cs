using PimpYourBlech_Contracts.Enums;

namespace PimpYourBlech_Contracts.EntityDTOs;

public class EngineDetailDto
{
    public int Id { get; set; }          // Primärschlüssel (PK)

    public int ProductId { get; set; }   // FK zu Product
    
    public Fuel Fuel { get; set; }
    
    public int Ps { get; set; } 
    public int Kw { get; set; } 
    
    public string Displacement{get;set;} 
    
    public Gear Gear { get; set; }
}