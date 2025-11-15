using Application.Menus;
using Application.Menus;
using TestAutoKonfigurator.Application.Menus.ConfiguratorMenus;
using TestAutoKonfigurator.Application.Menus.ConfiguratorMenus;
using TestAutoKonfigurator.Configorator;
using TestAutoKonfigurator.Inventories;
using TestAutoKonfigurator.Session;


namespace TestAutoKonfigurator;

public class App
{
    private readonly ICustomerInventory _customers;
    private readonly IProductInventory _products;
    private readonly ICarInventory _cars;
    private readonly IUserSession  _userSession;
    private readonly IConfiguratorService _configuratorService;

    public App(ICustomerInventory customerInventory,
        IProductInventory productInventory,
        ICarInventory carInventory,IUserSession  userSession, IConfiguratorService configuratorService)
    {
        _customers = customerInventory;
        _products  = productInventory;
        _cars      = carInventory;
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
                    var menu = new StartMenu(_customers, _products, _cars, _userSession);
                    _userSession.CurrentScreen = menu.Run();   // gibt Screen zurück damit weiß die app wo es nach dem jeweiligen menü weitergeht
                    break;
                }

                case Screens.MainMenu:
                {
                    var menu = new MainMenu(_customers, _products, _cars, _userSession);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }

                case Screens.AdminMenu:
                {
                    var menu = new AdminMenu(_customers, _products, _cars, _userSession);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }

                case Screens.ConfigStart:
                {
                    var menu = new ConfigStart(_customers, _products, _cars, _userSession, _configuratorService);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }

                case Screens.ConfigMain:
                {
                    var menu = new ConfigMain(_customers, _products, _cars, _userSession, _configuratorService);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }

                case Screens.ConfigEngine:
                {
                    var menu = new ConfigEngine(_products,_userSession, _configuratorService);
                    _userSession.CurrentScreen = menu.Run();
                    break;
                }

                case Screens.ConfigShow:
                {
                    var menu = new ConfigShow(_customers, _products, _cars, _userSession, _configuratorService);
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