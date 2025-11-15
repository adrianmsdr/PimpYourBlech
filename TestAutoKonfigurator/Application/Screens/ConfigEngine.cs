using TestAutoKonfigurator;
using TestAutoKonfigurator.Inventories;

namespace Application.Screens;

public class ConfigEngine(IProductInventory productInventory)
{
    public Screens Run()
    {
        while (true)
        {
            int index = 0;
            PrintHeader();
            foreach (Engine product in productInventory.ListProducts())
            {
                Console.WriteLine(product.Name);
            }

            Console.WriteLine("Motor wählen: ");
            Console.ReadKey();
            Console.WriteLine("Hier bin ich dann mal schlafen gegangen hahah");
            Console.ReadKey();
            return Screens.StartMenu;

        }
    }

    void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=== Motor ===");
        App.PrintSplitter();
    }
}