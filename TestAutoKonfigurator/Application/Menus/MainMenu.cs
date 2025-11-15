using TestAutoKonfigurator;
using TestAutoKonfigurator.Inventories;
using TestAutoKonfigurator.Session;

namespace Application.Menus;

public class MainMenu(
        ICustomerInventory customerInventory,
        IProductInventory productInventory,
        ICarInventory carInventory,
        IUserSession userSession)
{
        
        
        public Screens Run()
        {
                        bool running = true;
                        while (running)
                        {
                                PrintHeader();
                                Console.WriteLine("[1] Konfigurator");
                                Console.WriteLine("[2] Konfigurationen laden");
                                Console.WriteLine("[3] Ersatzteileshop");
                                Console.WriteLine("[4] Ausloggen");
                                        if (userSession.CurrentUser.AdminRights)
                                {
                                        Console.WriteLine("[5] Administrator");  }
                                
                                

                                App.PrintChooseOption();

                                string eingabe = Console.ReadKey().KeyChar.ToString();  
                                switch (eingabe)
                                {
                                        case "1":
                                                return Screens.ConfigStart;

                                        case "2":
                                                // Konfigurationen laden
                                                break;

                                        case "3":
                                                // Ersatzteileshop
                                                break;

                                        case "4":
                                               return Screens.StartMenu;
                                                break;


                                        case "5":
                                                if (userSession.CurrentUser.AdminRights)
                                                          {
                                                        return Screens.AdminMenu;
                                                  }
                                                break;

                                }
                        }
               return Screens.ExitMenu;
        }
        
        
        public static void PrintHeader()
        {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("=== PIMP YOUR BLECH ===");
                App.PrintSplitter();
        }
}