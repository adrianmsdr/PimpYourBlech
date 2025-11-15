using TestAutoKonfigurator;
using TestAutoKonfigurator.Configorator;
using TestAutoKonfigurator.Inventories;
using TestAutoKonfigurator.Session;

namespace Application.Screens;

public class ConfiguratorMenu(
ICustomerInventory customerInventory,
IProductInventory productInventory,
ICarInventory carInventory,
IUserSession userSession,
IConfiguratorService  configuratorService)

{
    public Screens Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Fahrzeug wählen ===");
            App.PrintSplitter();
            Console.WriteLine("[0] Hauptmenü");
            for (int i = 0; i < carInventory.ListCars().Count; i++)
            {
                Console.WriteLine("[" + (i+1) + "] " + carInventory.ListCars()[i].Name);
            }
            
            App.PrintChooseOption();
            
            string input = Console.ReadKey().KeyChar.ToString();
            
            if (!int.TryParse(input, out var index))
            {
                Console.WriteLine("Bitte eine gültige Zahl eingeben.");
                Console.ReadKey();
                return Screens.ConfigMenu;
            }
            
            

            if (input == "0")
            {
                return Screens.MainMenu;
            }

            if (index < 1 || index > carInventory.ListCars().Count)
            {
                Console.WriteLine("Bitte eine Zahl aus der Liste wählen.");
                Console.ReadKey();
                return Screens.ConfigMenu;
            }
            Car selectedCar = carInventory.ListCars()[index-1];
            Configuration configuration = configuratorService.StartNewConfiguration(userSession.CurrentUser, selectedCar); 
            PrintHeader();
            Console.WriteLine(selectedCar.ToString());
            App.PrintContinueMessage();
            Console.ReadKey();
            PrintHeader();
            Console.WriteLine("[1] Motor bearbeiten" +
                              "\n[2] Felgen bearbeiten" +
                              "\n[3] Farbe anpassen" +
                              "\n[4] Konfiguration löschen" +
                              "\n[5] Anderes Fahrzeug wählen" +
                              "\n[6] Hauptmenü");
            
            App.PrintChooseOption();
                string input2 = Console.ReadKey().KeyChar.ToString();
                switch (input2)
                {
                    case "1":
                        return Screens.ConfigEngine;
                        break;
                    
                    case "2":// Evtl mach ich hierfür noch eigne enum screens aber mal schauen das ist mies viel aufwand😂
                        break;
                    
                    case "3":
                        break;
                    
                    case "4":
                        break;
                    
                    case "5":
                        // Evtl mach ich hierfür noch eigne enum screens aber mal schauen das ist mies viel aufwand😂
                       return Screens.ConfigMenu;
                    
                    case "6":
                        return Screens.MainMenu;
                }
                
return Screens.ConfigMenu;
        }
    }


    public static void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
                
        Console.WriteLine("=== PIMP YOUR BLECH ===");
        App.PrintSplitter();
    }
    }
