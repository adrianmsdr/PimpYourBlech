using Application.Menus;
using TestAutoKonfigurator.Services.Configurator;
using TestAutoKonfigurator.Session;

namespace TestAutoKonfigurator.Application.Menus.ConfiguratorMenus;

public class ConfigLoad(IUserSession userSession, IConfiguratorService  configuratorService)
{
    public Screens Run()
    {
        while (true)
        {

            PrintHeader();
            Console.WriteLine("[0] <- zurück");
            for (int i = 0; i < configuratorService.GetAllConfigurations(userSession.CurrentUser).Count; i++)
            {
                Console.WriteLine("[" + (i + 1) + "] " +
                                  configuratorService.GetAllConfigurations(userSession.CurrentUser)[i]);
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
                return Screens.MainMenu;
            }

            if (index < 1 || index > configuratorService.GetAllConfigurations(userSession.CurrentUser).Count)
            {
                App.PrintSplitter();
                Console.WriteLine("Bitte eine Zahl aus der Liste wählen.");
                Console.ReadKey();
                break;
            }

            Configuration selectedConfig = configuratorService.GetAllConfigurations(userSession.CurrentUser)[index - 1];
            PrintHeader();
            Console.WriteLine(selectedConfig.ToString());
            App.PrintContinueMessage();
            Console.ReadKey();
            return Screens.MainMenu;

        }
        return Screens.MainMenu;
    }




    public void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
                
        Console.WriteLine("=== Konfigurationen verwalten ===");
        App.PrintSplitter();
    }
}
