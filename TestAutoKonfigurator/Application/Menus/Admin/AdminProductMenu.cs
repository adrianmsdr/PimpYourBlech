namespace Application.Menus;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
using System.Text;
using TestAutoKonfigurator;
using TestAutoKonfigurator.Exceptions;
using TestAutoKonfigurator.Inventories;
using TestAutoKonfigurator.Session;

public class AdminProductMenu(IProductInventory productInventory)
{
    // Menü zum Verwalten von Fahrzeugteilen
    public void Run()
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
    private void AddProductMenu() 
    
    {
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
    }
    
    public void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=== Ersatzteile verwalten ===");
        App.PrintSplitter();

    }
}