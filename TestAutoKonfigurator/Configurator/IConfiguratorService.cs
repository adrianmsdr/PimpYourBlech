namespace TestAutoKonfigurator.Configorator;

public interface IConfiguratorService
{
    
    // Konfiguration starten
    Configuration StartNewConfiguration(Customer customer, Car car);
    
    // Fahrzeugteil zur Konfiguration hinzufügen
    void AddProduct(Configuration configuration, Product product);
   
    // Fahrzeugteil von configuration entfernen
    void RemoveProduct(Configuration configuration, Product product);
    
    // Gesamtpreis der Konfiguration berechnen
    double CalculateTotalPrice(Configuration configuration);
   
    // Konfiguration speichern
    void SaveConfiguration(Configuration configuration);
    
}