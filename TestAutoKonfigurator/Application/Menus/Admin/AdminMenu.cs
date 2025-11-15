using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
using System.Text;
using TestAutoKonfigurator;
using TestAutoKonfigurator.Exceptions;
using TestAutoKonfigurator.Inventories;
using TestAutoKonfigurator.Session;

namespace Application.Menus;

public class AdminMenu
{
    private readonly AdminCustomerMenu _customerMenu;
    private readonly AdminProductMenu _productMenu;
    private readonly AdminCarMenu _carMenu;

    public AdminMenu(
        ICustomerInventory customerInventory,
        IProductInventory productInventory,
        ICarInventory carInventory,
        IUserSession userSession)
    {
        _customerMenu = new AdminCustomerMenu(customerInventory, userSession);
        _productMenu = new AdminProductMenu(productInventory);
        _carMenu = new AdminCarMenu(carInventory);
    }
    
    public Screens Run()
    {

        bool running = true;
        while (running)
        {
            PrintHeader();
            Console.WriteLine("[1] Kunden verwalten");
            Console.WriteLine("[2] Teile verwalten");
            Console.WriteLine("[3] Fahrzeuge verwalten");
            Console.WriteLine("[4] <- Zurück");
            TestAutoKonfigurator.App.PrintChooseOption();
            string eigabe = Console.ReadKey().KeyChar.ToString();
            switch (eigabe)
            {
                case "1":
                    _customerMenu.Run(); break;
                case "2":
                    _productMenu.Run(); break;
                case "3":
                    _carMenu.Run(); break;
                case "4":
                    running = false; break;
            }
        }
        return Screens.MainMenu;
    }
    public void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=== Administrator ===");
        App.PrintSplitter();
    }
}