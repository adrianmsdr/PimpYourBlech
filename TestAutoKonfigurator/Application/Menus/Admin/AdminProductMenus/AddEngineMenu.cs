using Application.Menus;
using TestAutoKonfigurator.Enums;
using TestAutoKonfigurator.Inventories;
using TestAutoKonfigurator.Services.Admin;

namespace TestAutoKonfigurator.Application.Menus.Admin.AdminProductMenus;

public class AddEngineMenu(IAdminService adminService)
{
    public Screens Run()
    {
        while (true)
        {
            PrintHeader();
            Console.Write("Name: ");
            string name = Console.ReadLine() ?? "";

            PrintHeader();
            Console.Write("Artikelnummer: ");
            string articleNumber = Console.ReadLine() ?? "";

            PrintHeader();
            Console.Write("Hersteller: ");
            string brand = Console.ReadLine() ?? "";

            PrintHeader();
            Console.Write("Beschreibung: ");
            string description = Console.ReadLine() ?? "";

            PrintHeader();
            Console.Write("Häufigkeit: ");
            int quantity = Convert.ToInt32(Console.ReadLine() ?? "0");

            PrintHeader();
            Console.Write("Preis: ");
            double price = Convert.ToDouble(Console.ReadLine() ?? "0");
            // return Screens.AdminMenu;

            PrintHeader();
            Console.Write("PS: ");
            int ps = Convert.ToInt32(Console.ReadLine() ?? "0");

            PrintHeader();
            Console.Write("Kw: ");
            int kw = Convert.ToInt32(Console.ReadLine() ?? "0");

            PrintHeader();
            Console.Write("Hubraum: ");
            string displacement = Console.ReadLine() ?? "";



            Gear gear = new Gear();
            bool running = true;
            while (running)
            {
                PrintHeader();
                Console.WriteLine("Wähle ein Getriebe: ");
                Console.WriteLine("[1] 5-Gang-Schaltgetriebe");
                Console.WriteLine("[2] 6-Gang-Schaltgetriebe");
                Console.WriteLine("[3] 6-Gang-Automatikgetriebe");
                Console.WriteLine("[4] 8-Gang-Automatikgetriebe");

                string input = Console.ReadKey().KeyChar.ToString();

                switch (input)
                {
                    case "1":
                        gear = Gear.FifeManual;
                        running = false;
                        break;

                    case "2":
                        gear = Gear.SixManual;
                        running = false;
                        break;

                    case "3":
                        gear = Gear.SixAutomatic;
                        running = false;
                        break;

                    case "4":
                        gear = Gear.EightAutomatic;
                        running = false;
                        break;

                    default:
                        App.PrintWrongInput();
                        Console.ReadKey();
                        break;
                }
            }
            
           adminService.RegisterEngine(name, articleNumber, brand, description, quantity, price, ps, kw, displacement,
               gear);
            PrintHeader();
            Console.WriteLine("Motor erfolgreich hinzugefügt.");
            App.PrintContinueMessage();
            Console.ReadKey();
            return Screens.AdminProductMenu;
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