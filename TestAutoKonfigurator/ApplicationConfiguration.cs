
using TestAutoKonfigurator;
using TestAutoKonfigurator.Database;
using TestAutoKonfigurator.Factories;
using TestAutoKonfigurator.Interfaces;
using TestAutoKonfigurator.Interfaces.Database;


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
        
        // App an sich wird erstellt und die Inventare werden übergeben
        var app = new Application(customerInventory, productInventory, carInventory);

        // Start der App
        app.Start();
        
        
        
        
        
        
    }
}