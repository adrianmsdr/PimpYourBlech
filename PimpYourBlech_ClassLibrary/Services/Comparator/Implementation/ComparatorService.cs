using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Data.Inventories.Implementation;

namespace PimpYourBlech_ClassLibrary.Services.Comparator.Implementation;

using PimpYourBlech_ClassLibrary.Services.Comparator.Models;

public class ComparatorService : IComparatorService
{
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
                Row("Leistung", configurations, c => $"{c.TotalPs} PS"),
                Row("Preis", configurations, c => $"{c.TotalPrice:N0} €"),
                Row("Verfügbarkeit", configurations,
                    c => c.Car.Quantity > 0 ? "Verfügbar" : "Nicht verfügbar")
            }
        };
    }

    private ComparisonRow Row<T>(
        string label,
        List<T> items,
        Func<T, string> selector)
    {
        return new ComparisonRow
        {
            Label = label,
            Values = items.Select(i => selector(i) ?? "-").ToList()
        };
    }
}