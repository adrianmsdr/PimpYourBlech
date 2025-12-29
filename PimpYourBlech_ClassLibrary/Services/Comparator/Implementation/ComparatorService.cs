namespace PimpYourBlech_ClassLibrary.Services.Comparator.Implementation;

using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Services.Comparator.Models;

public class ComparatorService : IComparatorService
{
    public ComparisonResult CompareCars(List<Car> cars)
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
                Row("Verfügbarkeit", cars, c => c.Quantity > 0 ? "Verfügbar" : "Nicht verfügbar")
            }
        };
    }

    private ComparisonRow Row(
        string label,
        List<Car> cars,
        Func<Car, string> selector)
    {
        return new ComparisonRow
        {
            Label = label,
            Values = cars.Select(selector).ToList()
        };
    }
}