namespace PimpYourBlech_ClassLibrary.Services.Comparator.Models;

public class ComparisonRow
{
    // Bezeichnung des Vergleichskriteriums (z. B. Preis, Leistung)
    public string Label { get; set; }
    
    // Werte für jedes verglichene Objekt in derselben Reihenfolge
    public List<string> Values { get; set; } = new();
}