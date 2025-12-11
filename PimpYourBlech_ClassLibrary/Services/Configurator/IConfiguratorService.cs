using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Services.Configurator;

public interface IConfiguratorService
{
    Car GetCarById(int carId);
    // Konfiguration starten
    Configuration StartNewConfiguration(Customer customer, Car car,string name);
    
    // Fahrzeugteil zur Konfiguration hinzufügen
    void AddProduct(Configuration configuration, Product product);
   
    // Fahrzeugteil von configuration entfernen
    void RemoveProduct(Configuration configuration, Product product);
    
    // Gesamtpreis der Konfiguration berechnen
    double CalculateTotalPrice(Configuration configuration);
   
    // Konfiguration speichern
    void SaveConfiguration(Configuration configuration);
    
    void DeleteConfiguration(Configuration configuration, Customer customer);
    
    
    List<Configuration> GetAllConfigurations(Customer customer);
    
    // im ConfiguratorService
    public void SaveConfiguration(Configuration configuration, Customer customer);
    
    public List<Product> ListEngines();
    
    public List<Car> ListCars();

}