using System.Security.Cryptography;
using System.Text;
//using TestAutoKonfigurator.Database;
using TestAutoKonfigurator.Exceptions;
//using TestAutoKonfigurator.Factories;
using TestAutoKonfigurator.Interfaces;
using TestAutoKonfigurator.Menus;

namespace TestAutoKonfigurator;

public sealed class Application(ICustomerInventory customerInventory,IProductInventory productInventory,ICarInventory carInventory)
{
    
    private readonly AdminMenu _adminMenu = new AdminMenu(customerInventory,productInventory,carInventory);

    // Startmenü der App
    public void StartMenu()
    {
        bool _running = true;
        while (_running)
        {

            PrintHeader();
            Console.WriteLine("1) Registrieren");
            Console.WriteLine("2) Anmelden");
            Console.WriteLine("3) Beenden");
            PrintChooseOption();

            string eingabe = Console.ReadLine() ?? "";

            switch (eingabe)
            {
                case "1":
                    RegistrationsMenu();
                    break;

                case "2":
                    LoginMenu();
                    break;

                case "3":
                    
                    _running = false;
                    break;


                default:
                    Console.WriteLine("Ungültige Eingabe. Drücke eine Taste...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    
    // Registrierung - Maske
    private void RegistrationsMenu()
    {
        bool running = true;
        while (running)
        {

            Console.Clear();

            PrintHeader();
            Console.Write("Vorname: ");
            string firstName = Console.ReadLine() ?? "";
            Console.Write("Nachname: ");
            string lastName = Console.ReadLine() ?? "";
            Console.Write("Username: ");
            string username = Console.ReadLine() ?? "";
            //LoginBlockedChecker();
            Console.Write("Passwort: ");
            string password = Console.ReadLine() ?? "";
            string hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
            Console.Write("Phone: ");
            string phone = Console.ReadLine() ?? "";

            Customer c = new Customer(firstName, lastName, username, hash, phone);

            try
            {
                AddCustomer(c);
                running = false;
            }
            catch (UsernameNotAvailableException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("1) Erneut versuchen");
                Console.WriteLine("2) Hauptmenü");
                PrintChooseOption();
                string eingabe = Console.ReadLine()!;
                if (eingabe == "1")
                {
                    running = true;
                }
                else if (eingabe == "2")
                {
                    running = false;
                }
            }

        }

    }

    
    // Login - Maske
    private void LoginMenu()
    {

        PrintHeader();
        Console.Write("Username: ");
        string username = Console.ReadLine() ?? "";
        Console.Write("Passwort: ");
        string password = Console.ReadLine()!;
        string hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
        if (!customerInventory.LoginBlockedChecker(username, hash))
        {
            customerInventory.GetCustomerAccount(username, hash);
            Console.WriteLine("Anmeldung erfolgreich. Enter drücken um fortzufahren.");
            Console.ReadKey();
            PrintMainMenu(customerInventory.GetCustomerAccount(username, hash).AdminRights);

        }
        else
        {
            Console.WriteLine("Anmeldung fehlgeschlagen. Bitte erneut versuchen.");
        }

        PrintReturnMessage();
    }
    
    
    
    // Hinzufügen eines Kunden
    private void AddCustomer(Customer c)
    {
        bool usernameTaken = false;

        if (c.Username.Length < 8)
        {
            usernameTaken = true;
            throw new UsernameNotAvailableException("Username zu kurz. Mindestens 8 Zeichen.");

        }

        if (customerInventory.ListCustomerUsernames().Contains(c.Username))
        {
            usernameTaken = true;
            throw new UsernameNotAvailableException("Username ist bereits vergeben, bitte wähle einen anderen!");
        }


        customerInventory.InsertCustomer(c);
        Console.WriteLine("Registrierung erfolgreich.");
        Console.WriteLine("Drücke Enter um fortzufahren oder [1] um deine Daten anzeigen zu lassen");

        string eingabe = Console.ReadLine() ?? "";
        switch (eingabe)
        {
            case "1":
                Console.WriteLine(c.ToString());
                PrintReturnMessage();
                Console.ReadKey();
                break;

            default:
                PrintReturnMessage();
                Console.ReadKey();
                break;
        }

    }
    

    // // Konsolenausgabe zum Zurückkehren
    public void PrintReturnMessage()
    {
        Console.WriteLine("Enter drücken um zurück zu gelangen");
        PrintChooseOption();
    }

    public void PrintHeader()
    {
        Console.Clear();
        Console.WriteLine("=== PIMP YOUR BLECH ===");
        Console.WriteLine("-----------------------");
    }

    private void PrintChooseOption()
    {
        Console.WriteLine();
        Console.Write("Auswahl: ");
        
        
    }

    private void PrintMainMenu(bool admin)
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            PrintHeader();
            Console.WriteLine("1) Konfigurator");
            Console.WriteLine("2) Konfigurationen laden");
            Console.WriteLine("3) Ersatzteileshop");
            Console.WriteLine("4) Ausloggen");
            if (admin)
            {
                Console.WriteLine("5) Administrator");
            }
            
            PrintChooseOption();

            string eingabe = Console.ReadLine() ?? "";
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


    
}