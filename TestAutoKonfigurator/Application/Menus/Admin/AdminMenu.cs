using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
using System.Text;
using TestAutoKonfigurator;
using TestAutoKonfigurator.Exceptions;
using TestAutoKonfigurator.Inventories;
using TestAutoKonfigurator.Session;

namespace Application.Menus;

public class AdminMenu()
    {
    
    public Screens Run()
    {
        
        while (true)
        {
            PrintHeader();
            Console.WriteLine("[1] Kunden verwalten");
            Console.WriteLine("[2] Teile verwalten");
            Console.WriteLine("[3] Fahrzeuge verwalten");
            Console.WriteLine("[4] <- Zurück");
            TestAutoKonfigurator.App.PrintChooseOption();
            string eigabe = Console.ReadKey().KeyChar.ToString();
            switch (eigabe)
            {
                case "1":
                    return Screens.AdminCustomerMenu;
                case "2":
                    return Screens.AdminProductMenu;
                case "3":
                    return Screens.AdminCarMenu;
                
                case "4":
                    return Screens.MainMenu;
            }
        }
    }
    public void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=== Administrator ===");
        App.PrintSplitter();
    }
}