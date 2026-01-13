using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Enums;

namespace PimpYourBlech_ClassLibrary.Services.Configurator;

public interface IConfiguratorService
{
    Task<Car> GetCarByIdAsync(int carId);
    // Konfiguration starten
    Configuration StartNewConfiguration(Customer customer, Car car,string name);

    public List<Configuration> GetAllConfigurationsForCustomer(int customerId);
    
    // Fahrzeugteil zur Konfiguration hinzufügen
    Task AddProduct(int configurationId, int productId);

    // Gesamtpreis der Konfiguration berechnen
    decimal CalculateTotalPrice(Configuration configuration);
   
    // Konfiguration speichern
    void SaveConfiguration(Configuration configuration);
    
    void DeleteConfiguration(Configuration configuration);
    
    
    List<Configuration> GetAllConfigurations(Customer customer);
    
    // im ConfiguratorService
    public void SaveConfiguration(Configuration configuration, Customer customer);
    
    public List<Product> ListEngines();
    
    public List<Product> ListRims();

    
    public Task<List<Car>> ListCarsAsync();
    
    Configuration GetConfigurationById(int Id);
    
    public Task<List<Product>> GetAvailableColorsAsync(int Id);
    
    public Task<List<Product>> GetAvailableEnginesAsync(int Id);
    public List<Product> GetAvailableRims(int Id);
    
    public Product GetProductById(int Id);

    public string GetGearDisplayName(Gear gear);

    public List<Product> GetAvailableExtras(int carId);
    
    public Task<List<Product>> GetAvailableProductsAsync(int carId,ProductType type);
    Task<bool> ConfigurationAvailable(int carId);
    
    Task<List<Car>> GetAvailableCarsAsync();

}