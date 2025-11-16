using Application.Menus;

namespace TestAutoKonfigurator.Application.Menus.Admin.AdminProductMenus;

public class AddEngineMenu
{
    public Screens Run()
    {
        PrintHeader();
        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        PrintHeader();
    Console.Write("Artikelnumer: ");
    string articleNumber = Console.ReadLine() ?? "";

    PrintHeader();
    Console.Write("Hersteller: ");
    string hersteller = Console.ReadLine() ?? "";

    PrintHeader();
    Console.Write("Beschreibung: ");
    string description = Console.ReadLine() ?? "";
    
    PrintHeader();
    Console.Write("Häufigkeit: ");
    int Quantity = Convert.ToInt32(Console.ReadLine() ?? "0");
    
    PrintHeader();
    Console.Write("Preis: ");
    double Price = Convert.ToDouble(Console.ReadLine() ?? "0");
    return Screens.AdminMenu;
    }
    
    public void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=== Administrator ===");
        App.PrintSplitter();
    }
}