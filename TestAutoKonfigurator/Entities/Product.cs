namespace TestAutoKonfigurator;

public class Product
{
    public string Name { get; set; }
    public string ArticleNumber{ get; set; }
    public string Brand { get; set; }
    public string Description{get;set;}
    public int Quantity { get; set; }
    public double Price { get; set; }

    public override string ToString()
    {
        return "Name: " + Name + "\nArtikelnummer: " + ArticleNumber + "\nHersteller: " + Brand + "\nAuf Lager: " + Quantity + "\nPreis: " +  Price + "\nBeschreibung: " +  Description;
    }
}