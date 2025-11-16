using Application.Menus;
using TestAutoKonfigurator.Inventories;

namespace TestAutoKonfigurator.Application.Menus.Admin.AdminProductMenus;

public class AdminProductMenu(IProductInventory productInventory)
{
    // Menü zum Verwalten von Fahrzeugteilen
    public Screens Run()
    {
        bool running = true;
        while (running)
        {
            PrintHeader();
            Console.WriteLine("1) Fahrzeugteileliste anzeigen");
            Console.WriteLine("2) Fahrzeugteil hinzufügen");
            Console.WriteLine("3) Fahrzeugteil suchen");
            Console.WriteLine("4) Fahrzeugteileliste löschen");
            TestAutoKonfigurator.App.PrintContinueMessage();

            string eingabe = Console.ReadLine() ?? "";

            switch (eingabe)
            {
                case "1":
                    ListProducts();
                    break;

                case "2":
                    AddProductMenu();
                    break;

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
        foreach (Product c in productInventory.ListProducts())
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
        
        PrintHeader();
        Console.WriteLine("[1] Motor hinzufügen");
        Console.WriteLine("[2] Felge hinzufügen");
        Console.WriteLine("[3] Lackfarbe hinzufügen");
        Console.WriteLine("[4] Spoiler hinzufügen");
        Console.WriteLine("[5] Scheinwerfer hinzufügen");
        Console.WriteLine("[6] Auspuff hinzufügen");
        Console.WriteLine("[7] Bremse hinzufügen");
        App.PrintChooseOption();
        
        string eingabe = Console.ReadKey().KeyChar.ToString() ?? "";

        if (eingabe == "1")
        {
            return Screens.AddEngineMenu;
        }
            
        


        PrintHeader();
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

        App.PrintContinueMessage();
        Console.ReadKey();
        return Screens.AdminMenu;
    }
    
    public void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=== Ersatzteile verwalten ===");
        App.PrintSplitter();

    }
}