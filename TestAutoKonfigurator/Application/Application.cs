using System.Security.Cryptography;
using System.Text;
//using TestAutoKonfigurator.Database;
using TestAutoKonfigurator.Exceptions;
//using TestAutoKonfigurator.Factories;
using TestAutoKonfigurator.Interfaces;
using TestAutoKonfigurator.Interfaces.Inventories;
using TestAutoKonfigurator.Menus;

namespace TestAutoKonfigurator;

public class Application(
    ICustomerInventory customerInventory,
    IProductInventory productInventory,
    ICarInventory carInventory)
{

    private readonly StartMenu _startMenu = new(customerInventory, productInventory, carInventory);


    // Startmenü der App
    public void Start()
    {
        _startMenu.Start();

    }


    //------------------------------------------- DESIGNELEMENTE ------------------------------------------------

    // // Konsolenausgabe zum Zurückkehren
    public static void PrintReturnMessage()
    {
        Console.WriteLine("Enter drücken um zurück zu gelangen");
        Console.WriteLine();
        Console.Write("Auswahl: ");
    }

    public static void PrintChooseOption()
    {
        Application.PrintSplitter();
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