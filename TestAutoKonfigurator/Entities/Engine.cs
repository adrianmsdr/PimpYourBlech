namespace TestAutoKonfigurator;

public class Engine(int _ps, int _kw):Product
{
    public int Ps { get; set; } = _ps;
    public int Kw { get; set; } = _kw;

    public override string ToString()
    {
        return "Name: " + Name + "Artikelnummer: " + ArticleNumber + "\nPs: " + Ps + "\nKw: " + Kw + "\nHersteller: " + Brand + "\nBeschreibung: " +  Description;
    }
}