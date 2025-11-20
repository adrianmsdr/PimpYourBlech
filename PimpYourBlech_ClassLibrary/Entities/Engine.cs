using PimpYourBlech_ClassLibrary.Enums;

namespace PimpYourBlech_ClassLibrary.Entities;

public class Engine:Product{



   
    public int Ps { get; set; } 
    public int Kw { get; set; } 
    
    public string Displacement{get;set;} 
    
    public Gear Gear { get; set; }

    
}