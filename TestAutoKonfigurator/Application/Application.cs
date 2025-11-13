using System.Security.Cryptography;
using System.Text;
//using TestAutoKonfigurator.Database;
using TestAutoKonfigurator.Exceptions;
//using TestAutoKonfigurator.Factories;
using TestAutoKonfigurator.Interfaces;
using TestAutoKonfigurator.Menus;

namespace TestAutoKonfigurator;

public class Application(ICustomerInventory customerInventory,IProductInventory productInventory,ICarInventory carInventory)
{
    
    private readonly StartMenu _startMenu = new (customerInventory,productInventory,carInventory);
    
    
    // Startmenü der App
   public void Start()
    {
        bool _running = true;
        while (_running)
        { 
            _startMenu.Start();
            
            }
        
    }

   
    

    // // Konsolenausgabe zum Zurückkehren
    public static void PrintReturnMessage()
    {
        Console.WriteLine("Enter drücken um zurück zu gelangen");
        Console.WriteLine();
        Console.Write("Auswahl: ");
    }

    public static void PrintChooseOption()
    {
        Console.WriteLine();
        Console.Write("Auswahl: ");
        
        
    }

    public static void PrintWrongInput()
    {
        Console.WriteLine();
        Console.WriteLine("Ungültige Eingabe. Drücke eine Taste...");
    }
    
    
    public static void PrintSplitter()
    {
        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
    }


    
}