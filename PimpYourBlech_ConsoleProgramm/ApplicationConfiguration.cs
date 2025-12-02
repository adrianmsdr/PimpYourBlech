
using PimpYourBlech_ClassLibrary.Factories;
using PimpYourBlech_ClassLibrary.Persistence;
using PimpYourBlech_ClassLibrary.Persistence.EFDatabase;
using PimpYourBlech_ClassLibrary.Services.Admin;
using PimpYourBlech_ClassLibrary.Services.Configurator;
using PimpYourBlech_ClassLibrary.Services.Configurator.Implementation;
using PimpYourBlech_ClassLibrary.Services.CustomerCommunication;
using TestAutoKonfigurator;
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
        
        IEmailService  emailService = new EmailService();
        
        IAdminService adminService = new AdminService(customerInventory, productInventory, carInventory, emailService);
        
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