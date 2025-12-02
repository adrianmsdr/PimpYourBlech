using Application.Menus;
using PimpYourBlech_ClassLibrary.Session;

namespace TestAutoKonfigurator.Application.Menus.ConfiguratorMenus;

public class ConfigMain(
IUserSession userSession)

{
    public Screens Run()
    {
        while (true)
        {
            
            PrintHeader();
            Console.WriteLine("[1] Motor bearbeiten" +
                              "\n[2] Felgen bearbeiten" +
                              "\n[3] Farbe anpassen" +
                              "\n[4] Konfiguration löschen" +
                              "\n[5] Konfiguration anzeigen" +
                              "\n[6] Anderes Fahrzeug wählen" +
                              "\n[7] Hauptmenü");
            
            App.PrintChooseOption();
                string input2 = Console.ReadKey().KeyChar.ToString();
                switch (input2)
                {
                    case "1":
                        return Screens.ConfigEngine;
                    
                    case "2":// Evtl mach ich hierfür noch eigne enum screens aber mal schauen das ist mies viel aufwand😂
                        break;
                    
                    case "3": // Über farben iteriieren 
                        
                    
                    case "4":
                        return Screens.ConfigDelete;
                    
                    case "5":
                        return Screens.ConfigShow;
                    
                    
                    case "6":
                        return Screens.ConfigStart;
                    
                    case "7":
                        return Screens.MainMenu;
                }
                
return Screens.ConfigMain;
        }
    }


    public void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
                
        Console.WriteLine("=== " + userSession.CurrentConfiguration.Name + " ===");
        App.PrintSplitter();
    }
    }
