using TestAutoKonfigurator.Enums;

namespace TestAutoKonfigurator;

public class Product
{
    public int Id { get; set; }
// Allgemeine Eigenschaften
    public string Name { get; set; }
    public string ArticleNumber { get; set; }
    public string Brand { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public ProductCategory Type { get; set; }

// Engine-Properties
    public int? Ps { get; set; }
    public int? Kw { get; set; }
    public string? Displacement { get; set; }
    public Gear? Gear { get; set; }

// Headlight-Properties
    public string? LightTechnology { get; set; }
    public int? Lumen { get; set; }

// Rim-Properties
    public int? Diameter { get; set; }

// Brake-Properties
    public string? BrakeType { get; set; }

}
/*(string name, string articleNumber, string brand, string description, int quantity, double price)
{
public string Name { get; set; } = name;
public string ArticleNumber{ get; set; }  = articleNumber;
public string Brand { get; set; }   = brand;
public string Description{get;set;}   = description;
public int Quantity { get; set; }   = quantity;
public double Price { get; set; }   = price;

public override string ToString()
{
return "Name: " + Name + "\nArtikelnummer: " + ArticleNumber + "\nHersteller: " + Brand + "\nAuf Lager: " + Quantity + "\nPreis: " +  Price + "\nBeschreibung: " +  Description;
}
}*/