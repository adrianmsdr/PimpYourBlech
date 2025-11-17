using TestAutoKonfigurator.Enums;

namespace TestAutoKonfigurator;

public class Engine:Product{
    
    /*(string name,
        string articleNumber,
        string brand,
        string description,
        int quantity,
        double price,
        int _ps,
        int _kw,
        string _displacement,
        Gear _gear
    )*/
    




   
    public int Ps { get; set; } 
    public int Kw { get; set; } 
    
    public string Displacement{get;set;} 
    
    public Gear Gear { get; set; }

    
}