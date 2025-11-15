using Application.Menus;
using TestAutoKonfigurator.Configorator;
using TestAutoKonfigurator.Inventories;
using TestAutoKonfigurator.Session;

namespace TestAutoKonfigurator.Application.Menus.ConfiguratorMenus;

public class ConfigShow(
ICustomerInventory customerInventory,
    IProductInventory productInventory,
ICarInventory carInventory,
    IUserSession userSession,
IConfiguratorService  configuratorService)
{
    public Screens Run()
    {
            PrintHeader();
            Console.WriteLine(userSession.CurrentConfiguration.ToString());
            App.PrintSplitter();
            App.PrintContinueMessage();
            Console.ReadKey();
            return Screens.MainMenu;
            
        
    }
    
    
    public void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
                
        Console.WriteLine("=== " + userSession.CurrentConfiguration.Name + " ===");
        App.PrintSplitter();
    }
}