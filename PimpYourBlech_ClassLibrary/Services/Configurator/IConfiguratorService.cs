using PimpYourBlech_Contracts.DTOs;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Enums;


namespace PimpYourBlech_ClassLibrary.Services.Configurator;

public interface IConfiguratorService
{
    // Startet eine neue Konfiguration (wird mit Namen und Standartprodukten persistiert)
    public Task<ConfigurationDto> StartNewConfiguration(CustomerDto customer, CarDto car, string name);

    // Getter für alle Konfigurationen eines Users
    Task<List<ConfigurationDto>> GetAllConfigurationsForCustomerAsync(int customerId);

    // Logik für das Hinzufügen/Ersetzen/Löschen eines Produktes an einer Konfiguration
    public Task HandleProductAsync(int configurationId, int productId);

    // Preisberechnung für eine Konfiguration
    public Task<decimal> CalculateTotalPriceAsync(ConfigurationDto configuration);

    // Konfiguration speichern/updaten
    public Task SaveConfigurationAsync(ConfigurationDto configuration, CustomerDto customer);

    // Konfiguration löschen
    public Task DeleteConfiguration(ConfigurationDto configuration);

    // Getter für eine Konfiguration über seine Id
    public Task<ConfigurationDto?> GetConfigurationByIdAsync(int configurationId);

    // Getter für verfügbare Motoren für ein Auto (Extra - Informationen in der Vorschau)
    public Task<List<ProductDto>> GetAvailableEnginesAsync(int carId);
    
    public Task<List<ProductDto>> GetAvailableLightsAsync(int carId);


    // Getter für den Anzeigenamen des Getriebes
    public string GetGearDisplayName(Gear gear);

   
    // Getter für alle verfügbaren Extras eines Fahrzeugs
    public Task<List<ProductDto>> GetAvailableExtras(int carId);

    // Query um verfügbare Produkte eines Fahrzeugs nach Produkttyp zu suchen
    public Task<List<ProductDto>> GetAvailableProductsAsync(int carId, ProductType type);
    
    
    // Getter für alle aktuell verfügbaren Fahrzeuge
    public Task<List<CarDto>> GetAvailableCarsAsync();

    // Liefert die Basis-Konfigurationsdaten für alle Fahrzeuge

    
    // Liefert die Basis-Konfigurationsdaten eines Kunden
    Task<List<ConfigurationCarItemsDto>> GetConfigurationsBasicsCustomerAsync(int customerId);

    // Liefert alle registrierten Produkte einer Konfiguration
    Task<List<ProductDto>> GetRegisteredProductsAsync(int configId);
}