using TestAutoKonfigurator.Interfaces;
using TestAutoKonfigurator.Interfaces.Inventories;

namespace TestAutoKonfigurator.Menus;

public class MainMenu(ICustomerInventory customerInventory,IProductInventory productInventory,ICarInventory  carInventory)
{
        
        private readonly AdminMenu _adminMenu = new AdminMenu(customerInventory, productInventory, carInventory);
        
        public void Start(bool admin)
        {
                        bool running = true;
                        while (running)
                        {
                                PrintHeader();
                                Console.WriteLine("[1] Konfigurator");
                                Console.WriteLine("[2] Konfigurationen laden");
                                Console.WriteLine("[3] Ersatzteileshop");
                                Console.WriteLine("[4] Ausloggen");
                                if (admin)
                                {
                                        Console.WriteLine("[5] Administrator");
                                }

                                Application.PrintChooseOption();

                                string eingabe = Console.ReadKey().KeyChar.ToString();  
                                switch (eingabe)
                                {
                                        case "1":
                                                // Konfigurator
                                                break;

                                        case "2":
                                                // Konfigurationen laden
                                                break;

                                        case "3":
                                                // Ersatzteileshop
                                                break;

                                        case "4":
                                                running = false;
                                                break;


                                        case "5":
                                                if (admin)
                                                {
                                                        _adminMenu.Start();
                      
                                                }

                                                break;
                                        

                                }
                        }
                
        }
        
        
        public static void PrintHeader()
        {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("=== PIMP YOUR BLECH ===");
                Application.PrintSplitter();
        }
}