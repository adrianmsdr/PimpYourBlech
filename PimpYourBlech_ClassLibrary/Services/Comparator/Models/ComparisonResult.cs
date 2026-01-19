
using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.Services.Comparator.Models;



public class ComparisonResult
{
    
    // Enthält die verglichenen Fahrzeuge,
    public List<CarDto> Cars { get; set; } = new();
    
    // Repräsentiert die Vergleichszeilen der tabelle
    public List<ComparisonRow> Rows { get; set; } = new();
}
public class ConfigurationComparisonResult
{
    
    // Enthält die verglichenen Konfigurationen
    public List<ConfigurationDto> Configurations { get; set; } = new();
    
    // Repräsentiert die Vergleichszeilen der tabelle
    public List<ComparisonRow> Rows { get; set; } = new();
}