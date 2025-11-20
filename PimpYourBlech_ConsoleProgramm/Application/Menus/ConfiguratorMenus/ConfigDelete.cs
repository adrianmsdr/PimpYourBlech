using Application.Menus;
using PimpYourBlech_ClassLibrary.Services.Configurator;
using TestAutoKonfigurator.Session;

namespace TestAutoKonfigurator.Application.Menus.ConfiguratorMenus;

public class ConfigDelete(
    IUserSession userSession,
    IConfiguratorService configuratorService)
{
    public Screens Run()
    {
        while (true)
        {
            PrintHeader();
            Console.WriteLine(" Möchtest du die aktuelle Konfiguration wirklich löschen? [Y/N]");
            App.PrintChooseOption();
            string input = Console.ReadKey().KeyChar.ToString();
            if (input == "Y" || input == "y")
            {
               configuratorService.DeleteConfiguration(userSession.CurrentConfiguration, userSession.CurrentUser);
              PrintHeader();
              Console.WriteLine("Konfiguration erfolgreich gelöscht.");
              App.PrintContinueMessage();
              Console.ReadKey();
              return Screens.MainMenu;
            }
            if (input == "N" || input == "n")
            {
                 PrintHeader();
                 Console.WriteLine("Löschvorgang erfolgreich abgebrochen. ");
                 App.PrintContinueMessage();
                 Console.ReadKey();
                 return Screens.ConfigMain;
            }
            App.PrintWrongInput();
            Console.ReadKey();
        }
    }

    private void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
                
        Console.WriteLine("=== " + userSession.CurrentConfiguration.Name + " ===");
        App.PrintSplitter();
        
        
    }
}