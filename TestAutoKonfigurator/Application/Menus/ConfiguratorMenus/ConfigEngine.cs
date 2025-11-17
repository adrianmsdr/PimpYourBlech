using Application.Menus;
using TestAutoKonfigurator.Configorator;
using TestAutoKonfigurator.Inventories;
using TestAutoKonfigurator.Session;
using TestAutoKonfigurator.Session.Implementation;

namespace TestAutoKonfigurator.Application.Menus.ConfiguratorMenus;

public class ConfigEngine(IProductInventory productInventory,IUserSession userSession, IConfiguratorService configuratorService)
{
    public Screens Run()
    {
        while (true)
        {
            
            PrintHeader();
            Console.WriteLine("[0] <-");
            Console.WriteLine("Products im Inventory: " + productInventory.ListEngines().Count);
            for (int i = 0; i < productInventory.ListEngines().Count; i++)
            {
                Console.WriteLine("[" + (i+1) + "] " + productInventory.ListEngines()[i].Name);
            }
            
            App.PrintChooseOption();
            
            string input = Console.ReadKey().KeyChar.ToString();
            
            if (!int.TryParse(input, out var index))
            {
                App.PrintSplitter();
                Console.WriteLine("Bitte eine gültige Zahl eingeben.");
                Console.ReadKey();
                break;
            }
            
            

            if (input == "0")
            {
                return Screens.ConfigMain;
            }

            if (index < 1 || index > productInventory.ListEngines().Count)
            {
                App.PrintSplitter();
                Console.WriteLine("Bitte eine Zahl aus der Liste wählen.");
                Console.ReadKey();
                break;
            }
            Product selectedEngine = productInventory.ListEngines()[index - 1];
            //userSession.CurrentConfiguration.Engine = selectedEngine;
            App.PrintSplitter();
            Console.WriteLine("Motor erfolgreich hinzugefügt. ");
            App.PrintContinueMessage();
            Console.ReadKey();
            return Screens.ConfigMain;

        }
        return Screens.ConfigMain;
    }

    void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=== Motor wählen ===");
        App.PrintSplitter();
    }
}