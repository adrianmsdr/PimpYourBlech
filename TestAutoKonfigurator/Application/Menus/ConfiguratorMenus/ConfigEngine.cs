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
            for (int i = 0; i < productInventory.ListEngines().Count; i++)
            {
                Console.WriteLine("[" + (i+1) + "] " + productInventory.ListEngines()[i].Name);
            }
            
            App.PrintChooseOption();
            
            string input = Console.ReadKey().KeyChar.ToString();
            
            if (!int.TryParse(input, out var index))
            {
                Console.WriteLine("Bitte eine gültige Zahl eingeben.");
                Console.ReadKey();
                return Screens.ConfigEngine;
            }
            
            

            if (input == "0")
            {
                return Screens.ConfigMain;
            }

            if (index < 1 || index > productInventory.ListEngines().Count)
            {
                Console.WriteLine("Bitte eine Zahl aus der Liste wählen.");
                Console.ReadKey();
                return Screens.ConfigEngine;
            }
            Engine selectedEngine = productInventory.ListEngines()[index - 1];
            userSession.CurrentConfiguration.Engine = selectedEngine;

        }
    }

    void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=== Motor wählen ===");
        App.PrintSplitter();
    }
}