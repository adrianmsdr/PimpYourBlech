
using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.Services.Comparator;

using PimpYourBlech_ClassLibrary.Services.Comparator.Models;

public interface IComparatorService
{
    ComparisonResult CompareCars(List<CarDto> cars);
    ComparisonResult CompareConfigurations(List<ConfigurationDto> configurations);
}