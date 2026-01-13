namespace PimpYourBlech_ClassLibrary.Services.Comparator;

using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Services.Comparator.Models;

public interface IComparatorService
{
    ComparisonResult CompareCars(List<Car> cars);
    ComparisonResult CompareConfigurations(List<Configuration> configurations);
}