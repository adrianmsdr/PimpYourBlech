using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.Services.Comparator.Implementation;

using PimpYourBlech_ClassLibrary.Services.Comparator.Models;

public class ComparatorService : IComparatorService
{
    
    // Vergleicht mehrere Fahrzeuge anhand ihrer Eigenschaften und gibt eine Liste dieser aus
    public ComparisonResult CompareCars(List<CarDto> cars)
    {
        return new ComparisonResult
        {
            Cars = cars,
            Rows = new List<ComparisonRow>
            {
                Row("Name", cars, c => c.Name),
                Row("Marke", cars, c => c.Brand),
                Row("Modell", cars, c => c.Model),
                Row("Baujahr", cars, c => c.DateProduction),
                Row("Erstzulassung", cars, c => c.DatePermit),
                Row("Leistung", cars, c => $"{c.PS} PS"),
                Row("Preis", cars, c => $"{c.Price:N0} €"),
                Row("Verfügbarkeit", cars,
                    c => c.Quantity > 0 ? "Verfügbar" : "Nicht verfügbar")
            }
        };
    }

    // Vergleicht gespeicherte Konfigurationen miteinander
    public ConfigurationComparisonResult CompareConfigurations(
        List<ConfigurationDto> configurations)
    {
        return new ConfigurationComparisonResult
        {
            Configurations = configurations,
            Rows = new List<ComparisonRow>
            {
                Row("Name", configurations, c => c.Car.Name),
                Row("Marke", configurations, c => c.Car.Brand),
                Row("Modell", configurations, c => c.Car.Model),
                Row("Baujahr", configurations, c => c.Car.DateProduction),
                Row("Erstzulassung", configurations, c => c.Car.DatePermit),
                // Gesamtleistung der Konfiguration
                Row("Leistung", configurations, c => $"{c.TotalPs} PS"),
                // Gesamtpreis inklusive aller gewählten Produkte
                Row("Preis", configurations, c => $"{c.TotalPrice:N0} €"),
                Row("Verfügbarkeit", configurations,
                    c => c.Car.Quantity > 0 ? "Verfügbar" : "Nicht verfügbar")
            }
        };
    }

    // Zentrale Hilfsmethode zum Erzeugen einer Vergleichszeile.
    // Sie kapselt die Mapping-Logik und vermeidet Code-Duplikate
    // zwischen Fahrzeug- und Konfigurationsvergleich.
    private ComparisonRow Row<T>(
        string label,
        List<T> items,
        Func<T, string> selector)
    {
        return new ComparisonRow
        {
            Label = label,
            
            
            // Für jedes Objekt wird genau ein Wert erzeugt.
            // Null-Werte werden bewusst abgefangen,
            // um Darstellungsfehler in der UI zu vermeiden.
            Values = items.Select(i => selector(i) ?? "-").ToList()
        };
    }
}