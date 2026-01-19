
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_ClassLibrary.Services.Comparator.Models;

namespace PimpYourBlech_ClassLibrary.Services.Comparator;

public interface IComparatorService
{
    
    // Vergleicht mehrere Fahrzeuge und bereitet die Daten
    // für die darstellung in der tabelle auf
    ComparisonResult CompareCars(List<CarDto> cars);

    // Vergleicht mehrere gespeicherte Konfigurationen
    // inklusive aggregierter Werte wie Gesamtpreis und Leistung
    ConfigurationComparisonResult CompareConfigurations(
        List<ConfigurationDto> configurations);
}