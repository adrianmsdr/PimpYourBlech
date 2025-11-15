using Application.Screens;
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
        var current = Screens.StartMenu;

        while (current != Screens.ExitMenu)
        {
            switch (current)
            {
                case Screens.StartMenu:
                {
                    var menu = new StartMenu(_customers, _products, _cars, _userSession);
                    current = menu.Run();   // gibt Screen zurück damit weiß die app wo es nach dem jeweiligen menü weitergeht
                    break;
                }

                case Screens.MainMenu:
                {
                    var menu = new MainMenu(_customers, _products, _cars, _userSession);
                    current = menu.Run();
                    break;
                }

                case Screens.AdminMenu:
                {
                    var menu = new AdminMenu(_customers, _products, _cars, _userSession);
                    current = menu.Run();
                    break;
                }

                case Screens.ConfigMenu:
                {
                    var menu = new ConfiguratorMenu(_customers, _products, _cars, _userSession, _configuratorService);
                    current = menu.Run();
                    break;
                }

                case Screens.ConfigEngine:
                {
                    var menu = new ConfigEngine(_products);
                    current = menu.Run();
                    break;
                }
                
            }
        }
    }
    


    //------------------------------------------- DESIGNELEMENTE ------------------------------------------------

    // // Konsolenausgabe zum Zurückkehren
    public static void PrintContinueMessage()
    {
        Console.WriteLine("Enter drücken um fortzufahren");
        Console.WriteLine();
        Console.Write("Auswahl: ");
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
            "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
    }



}