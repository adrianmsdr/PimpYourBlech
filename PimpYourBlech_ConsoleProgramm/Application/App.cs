using Application.Menus;
using Application.Menus;
using PimpYourBlech_ClassLibrary.Services.Admin;
using PimpYourBlech_ClassLibrary.Services.Configurator;
using PimpYourBlech_ClassLibrary.Session;
using TestAutoKonfigurator.Application.Menus.Admin.AdminCustomerMenus;
using TestAutoKonfigurator.Application.Menus.Admin.AdminProductMenus;
using TestAutoKonfigurator.Application.Menus.ConfiguratorMenus;
using TestAutoKonfigurator.Application.Menus.ConfiguratorMenus;


namespace TestAutoKonfigurator;

public class App
{
    private readonly IAdminService _adminService;
    private readonly IUserSession  _userSession;
    private readonly IConfiguratorService _configuratorService;

    public App(IAdminService adminService,IUserSession  userSession, IConfiguratorService configuratorService)
    {
        _adminService = adminService;
        _userSession = userSession;
        _configuratorService = configuratorService;
    }

    public void Start()
    {
        _userSession.CurrentScreen = Screens.StartMenu;

        while (_userSession.CurrentScreen != Screens.ExitMenu)
        {
            switch (_userSession.CurrentScreen)
            {
                case Screens.StartMenu:
                {
                    var menu = new StartMenu(_adminService, _userSession);
                    _userSession.CurrentScreen = menu.Run();   // gibt Screen zurück damit weiß die app wo es nach dem jeweiligen menü weitergeht
                    break;
                }

                case Screens.MainMenu:
                {
                    var menu = new MainMenu( _userSession);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }

                case Screens.AdminMenu:
                {
                    var menu = new AdminMenu();
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }

                case Screens.AdminCustomerMenu:
                {
                    var menu = new AdminCustomerMenu(_adminService);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }

                case Screens.AdminProductMenu:
                {
                    var menu = new AdminProductMenu(_adminService);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }
                
                case Screens.AddEngineMenu:
                {
                    var menu = new AddEngineMenu(_adminService);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }

                case Screens.AdminCarMenu:
                {
                    var menu = new AdminCarMenu(_adminService);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }

                case Screens.ConfigStart:
                {
                    var menu = new ConfigStart( _userSession, _configuratorService);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }

                case Screens.ConfigMain:
                {
                    var menu = new ConfigMain( _userSession);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }

                case Screens.ConfigEngine:
                {
                    var menu = new ConfigEngine(_userSession, _configuratorService);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }

                case Screens.ConfigDelete:
                {
                    var menu = new ConfigDelete(_userSession, _configuratorService);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }
                
                case Screens.ConfigShow:
                {
                    var menu = new ConfigShow( _userSession);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }
                
                case Screens.ConfigLoad:
                {
                    var menu = new ConfigLoad( _userSession, _configuratorService);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }
                
                case Screens.AddCustomerMenu:
                    {
                    var menu = new AddCustomerMenu(_adminService);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                    }
            }
        }
    }
    
    //------------------------------------------- DESIGNELEMENTE ------------------------------------------------

    // // Konsolenausgabe zum Zurückkehren
    public static void PrintContinueMessage()
    {
        PrintSplitter();
        Console.Write("Enter drücken um fortzufahren"); ;
    }

    public static void PrintChooseOption()
    {
        App.PrintSplitter();
        Console.Write("Auswahl: ");


    }

    public static void PrintWrongInput()
    {
        Console.WriteLine();
        Console.WriteLine("Ungültige Eingabe. Drücke eine Taste...");
    }
    
    public static void PrintSplitter()
    {
        Console.WriteLine(
            "---------------------------------------------");
    }
}