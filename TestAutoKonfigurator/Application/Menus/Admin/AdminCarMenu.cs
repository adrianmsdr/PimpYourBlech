using TestAutoKonfigurator.Services.Admin;

namespace Application.Menus;

using TestAutoKonfigurator;


public class AdminCarMenu(IAdminService adminService)
{

    // Fahrzeuge verwalten - Hauptmenü
    public Screens Run()
    {
        bool running = true;

        while (running)
        {
            PrintHeader();
            Console.WriteLine("[1] Fahrzeugliste anzeigen");
            Console.WriteLine("[2] Fahrzeug hinzufügen");
            Console.WriteLine("[3] Fahrzeug suchen");
            Console.WriteLine("[4] Fahrzeug löschen");
            Console.WriteLine("[5] <- Zurück ");
            
            App.PrintChooseOption();


            switch (Console.ReadKey().KeyChar.ToString())
            {
                case "1":
                    ListCarsMenu(); break;

                case "2":
                    AddCarMenu(); break;

                case "3":
                    SearchCarMenu(); break;

                case "4":
                    DeleteCarsMenu(); break;

                case "5":
                    running = false; break;
                
            }
        }
        return Screens.AdminMenu;
    }
    
    // Autos hinzufügen
    private void AddCarMenu()
    {
        PrintHeader();
        Console.Write("Name ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Hersteller: ");
        string brand = Console.ReadLine() ?? "";

        Console.Write("Modell: ");
        string model = Console.ReadLine() ?? "";

        Console.Write("Baujahr: ");
        string dateProduction = Console.ReadLine() ?? "";

        Console.Write("Erstzulassung: ");
        string datePermit = Console.ReadLine() ?? "";

        Console.Write("Leistung (PS): ");
        int ps = Convert.ToInt32(Console.ReadLine() ?? "");

        Console.Write("Häufigkeit: ");
        int quantity = Convert.ToInt32(Console.ReadLine());

        Console.Write("Preis: ");
        double price = Convert.ToDouble(Console.ReadLine());
        
        adminService.RegisterCar(name, dateProduction, datePermit, brand, model, ps, quantity, price);
        PrintHeader();
        Console.WriteLine("Fahrzeug erfolgreich hinzugefügt");
        App.PrintContinueMessage();
        Console.ReadKey();
    }
    
    public void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=== Fahrzeuge verwalten ===");
        App.PrintSplitter();
    }
    
    public void ListCarsMenu()
    {
        // To Do
    }

    public void SearchCarMenu()
    {
        // To Do
    }

    public void DeleteCarsMenu()
    {
        // To Do
    }
}