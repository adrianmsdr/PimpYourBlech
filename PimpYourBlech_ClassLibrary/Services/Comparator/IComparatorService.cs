
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_ClassLibrary.Services.Comparator.Models;

namespace PimpYourBlech_ClassLibrary.Services.Comparator;

public interface IComparatorService
{
    ComparisonResult CompareCars(List<CarDto> cars);

    ConfigurationComparisonResult CompareConfigurations(
        List<ConfigurationDto> configurations);
}