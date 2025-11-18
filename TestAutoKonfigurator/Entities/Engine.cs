using TestAutoKonfigurator.Enums;

namespace TestAutoKonfigurator;

public class Engine:Product{



   
    public int Ps { get; set; } 
    public int Kw { get; set; } 
    
    public string Displacement{get;set;} 
    
    public Gear Gear { get; set; }

    
}