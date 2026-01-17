
using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.Services.Comparator.Models;



public class ComparisonResult
{
    public List<CarDto> Cars { get; set; } = new();
    public List<ComparisonRow> Rows { get; set; } = new();
}
public class ConfigurationComparisonResult
{
    public List<ConfigurationDto> Configurations { get; set; } = new();
    public List<ComparisonRow> Rows { get; set; } = new();
}