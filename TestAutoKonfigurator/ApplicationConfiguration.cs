
using TestAutoKonfigurator;
using TestAutoKonfigurator.Factories;
using TestAutoKonfigurator.Inventories.Implementation;
using TestAutoKonfigurator.Persistence;
using TestAutoKonfigurator.Persistence.EFDatabase;
using TestAutoKonfigurator.Services;
using TestAutoKonfigurator.Services.Admin;
using TestAutoKonfigurator.Services.Configurator;
using TestAutoKonfigurator.Services.Configurator.Implementation;
using TestAutoKonfigurator.Session;
using TestAutoKonfigurator.Session.Implementation;


public static class ApplicationConfiguration
{
    public static void Main(string[] args)
    {

        // Instanz der Json - Datenbankklasse wird erstellt mit Zugriff über das Interface "IJsonDatabase"
       // IDatabase database = new JsonDatabase();
       var dbContext = new ConfiguratorContext();
       IDatabase database = new DatabaseEF(dbContext);
        // Factory wird erstellt & Datenbankschnittstelle wird übergehen
        var fac = new InventoryFactory(database);
        
        // Kundeninventar wird erstellt (geladen), Zugriff erfolgt über Schnittstelle ICustomerInventory die wir durch den getter aus der Factory erhalten 
        var customerInventory = fac.GetCustomerRepository();
       
        // Produktinventar wird erstellt (geladen), Zugriff erfolgt über Schnittstelle IProductInventory die wir durch den getter aus der Factory erhalten 
        var productInventory = fac.GetProductInventory();
        
        // Fahrzeuginventar wird erstellt (geladen), Zugriff erfolgt über Schnittstelle ICarInventory die wir durch den getter aus der Factory erhalten 
        var carInventory = fac.GetCarInventory(); 
        
        IAdminService adminService = new AdminService(customerInventory, productInventory, carInventory);
        
        //Benutzer Session wird erzeugt
        IUserSession userSession = new UserSession();
        
        // Configurator Schnittstelle wird erzeugt
        IConfiguratorService configuratorService = new ConfiguratorService( customerInventory, productInventory, carInventory);
        
        // App an sich wird erstellt und die Inventare, sowie Benutzersession werden übergeben
        var app = new App(adminService, userSession, configuratorService);

        
        // Start der App
        app.Start();
        
        
        
        
        
        
    }
}