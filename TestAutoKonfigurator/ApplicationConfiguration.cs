
using TestAutoKonfigurator;
using TestAutoKonfigurator.Configorator;
using TestAutoKonfigurator.Configorator.Implementation;
using TestAutoKonfigurator.Database;
using TestAutoKonfigurator.Database.Implementation;
using TestAutoKonfigurator.Factories;
using TestAutoKonfigurator.Inventories.Implementation;
using TestAutoKonfigurator.Inventories.InventoryService;
using TestAutoKonfigurator.Session;
using TestAutoKonfigurator.Session.Implementation;


public static class ApplicationConfiguration
{
    public static void Main(string[] args)
    {

        // Instanz der Json - Datenbankklasse wird erstellt mit Zugriff über das Interface "IJsonDatabase"
        IJsonDatabase database = new JsonDatabase();
        
        // Factory wird erstellt & Datenbankschnittstelle wird übergehen
        var fac = new InventoryFactory(database);
        
        // Kundeninventar wird erstellt (geladen), Zugriff erfolgt über Schnittstelle ICustomerInventory die wir durch den getter aus der Factory erhalten 
        var customerInventory = fac.GetCustomerRepository();
       
        // Produktinventar wird erstellt (geladen), Zugriff erfolgt über Schnittstelle IProductInventory die wir durch den getter aus der Factory erhalten 
        var productInventory = fac.GetProductInventory();
        
        // Fahrzeuginventar wird erstellt (geladen), Zugriff erfolgt über Schnittstelle ICarInventory die wir durch den getter aus der Factory erhalten 
        var carInventory = fac.GetCarInventory(); 
        
        ICustomerService customerService = new CustomerService(customerInventory);
        
        //Benutzer Session wird erzeugt
        IUserSession userSession = new UserSession();
        
        // Configurator Schnittstelle wird erzeugt
        IConfiguratorService configuratorService = new ConfiguratorService();
        
        // App an sich wird erstellt und die Inventare, sowie Benutzersession werden übergeben
        var app = new App(customerService, productInventory, carInventory, userSession, configuratorService);

        
        // Start der App
        app.Start();
        
        
        
        
        
        
    }
}