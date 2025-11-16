using Application.Menus;
using TestAutoKonfigurator.Configorator;
using TestAutoKonfigurator.Inventories;
using TestAutoKonfigurator.Session;

namespace TestAutoKonfigurator.Application.Menus.ConfiguratorMenus;

public class ConfigStart(
    ICarInventory carInventory,
    IUserSession userSession,
    IConfiguratorService configuratorService)

{
    public Screens Run()
    {
        while (true)
        {

            Console.Clear();
            Console.WriteLine("=== Fahrzeug wählen ===");
            App.PrintSplitter();
            Console.WriteLine("[0] <-");
            for (int i = 0; i < carInventory.ListCars().Count; i++)
            {
                Console.WriteLine("[" + (i + 1) + "] " + carInventory.ListCars()[i].Name);
            }

            App.PrintChooseOption();

            string input = Console.ReadKey().KeyChar.ToString();

            if (input == "0")
            {
                return Screens.MainMenu;
            }
            
            if (!int.TryParse(input, out var index))
            {
                Console.WriteLine("Bitte eine gültige Zahl eingeben.");
                Console.ReadKey();
                return Screens.ConfigStart;
            }

            if (index < 1 || index > carInventory.ListCars().Count)
            {
                Console.WriteLine("Bitte eine Zahl aus der Liste wählen.");
                Console.ReadKey();
                return Screens.ConfigStart;
            }
            

            

            Car selectedCar = carInventory.ListCars()[index - 1];
            PrintHeader();
            Console.Write("Name der Konfiguration: ");
            string name = Console.ReadLine() ?? "";
            Configuration configuration = configuratorService.StartNewConfiguration(userSession.CurrentUser, selectedCar, name);
            userSession.CurrentConfiguration = configuration;
            PrintHeader();
            Console.WriteLine(userSession.CurrentConfiguration.Car.ToString());
            App.PrintContinueMessage();
            Console.ReadKey();
            return Screens.ConfigMain;
        }
    }

    public void PrintHeader()
    {
        Console.Clear();
        Console.WriteLine("=== === PIMP YOUR BLECH === ===");
        App.PrintSplitter();
    }
}