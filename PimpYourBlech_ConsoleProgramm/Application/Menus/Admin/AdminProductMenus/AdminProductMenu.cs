using Application.Menus;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Services.Admin;

namespace TestAutoKonfigurator.Application.Menus.Admin.AdminProductMenus;

public class AdminProductMenu(IAdminService adminService)
{
    // Menü zum Verwalten von Fahrzeugteilen
    public Screens Run()
    {
        bool running = true;
        while (running)
        {
            PrintHeader();
            Console.WriteLine("[1] Fahrzeugteileliste anzeigen");
            Console.WriteLine("[2] Fahrzeugteil hinzufügen");
            Console.WriteLine("[3] Fahrzeugteil suchen");
            Console.WriteLine("[4] Fahrzeugteileliste löschen");
            Console.WriteLine("[5] <- zurück");
            
            App.PrintChooseOption();

            string eingabe = Console.ReadLine() ?? "";

            switch (eingabe)
            {
                case "1":
                    ListProducts();
                    break;

                case "2":
                    return AddProductMenu();
                    break;
                
                case "3":
                    
                case "4":
                        
                case "5":
                    return Screens.AdminMenu;

                default: 
                    running = false;
                    break;
            }
        }

        return Screens.AdminMenu;
    }
    
    // Alle Produkte anzeigen
    private void ListProducts()
    {
        PrintHeader();
        foreach (Product c in adminService.GetProducts())
        {
            Console.WriteLine(c.ToString());
            App.PrintSplitter();
        }

        App.PrintContinueMessage();
        Console.ReadKey();
    }
    
    // Hinzufügen von Produkten
    private Screens AddProductMenu() 
    
    {
        while (true)
        {


            PrintHeader();
            Console.WriteLine("[1] Motor hinzufügen");
            Console.WriteLine("[2] Felge hinzufügen");
            Console.WriteLine("[3] Lackfarbe hinzufügen");
            Console.WriteLine("[4] Spoiler hinzufügen");
            Console.WriteLine("[5] Scheinwerfer hinzufügen");
            Console.WriteLine("[6] Auspuff hinzufügen");
            Console.WriteLine("[7] Bremse hinzufügen");
            Console.WriteLine("[8] <- zurück");
            App.PrintChooseOption();

            string eingabe = Console.ReadKey().KeyChar.ToString() ?? "";

            switch (eingabe)
            {
                case "1":
                    return Screens.AddEngineMenu;

                case "2":
                //return Screens.AddRimMenu;

                case "3":
                //return Screens.AddColorMenu;

                case "4":
                //return Screens.AddSpoilerMenu;

                case "5":
                //return Screens.AddLightsMenu;

                case "6":
                //return Screens.AddExhaustMenu;

                case "7":
                //return Screens.AddBrakes;

                case "8":
                    return Screens.AdminProductMenu;
            }
        }





       /* PrintHeader();
        Console.Write("Artikelnummer: ");
        string articleNumber = Console.ReadLine()!;
        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Hersteller: ");
        string brand = Console.ReadLine();
        Console.Write("Häufigkeit: ");
        int quantity = Convert.ToInt32(Console.ReadLine());
        Console.Write("Preis: ");
        double price = Convert.ToDouble(Console.ReadLine());
        Console.Write("Beschreibung: ");
        string description = Console.ReadLine();
        

        Product p = new Product();
        p.ArticleNumber = articleNumber;
        p.Name = name;
        p.Brand = brand;
        p.Quantity = quantity;
        p.Description = description;
        p.Price = price;
       
        
        productInventory.InsertProduct(p);
*/
        App.PrintContinueMessage();
        Console.ReadKey();
       // return Screens.AdminMenu;
    }
    
    public void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=== Ersatzteile verwalten ===");
        App.PrintSplitter();

    }
}