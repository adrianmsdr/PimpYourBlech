using System.Security.Cryptography;
using System.Text;
using TestAutoKonfigurator;
using TestAutoKonfigurator.CustomerCommunication;
using TestAutoKonfigurator.Exceptions;
using TestAutoKonfigurator.Inventories;
using TestAutoKonfigurator.Inventories.InventoryService;
using TestAutoKonfigurator.Session;

namespace Application.Menus;

public class StartMenu(ICustomerService service, IUserSession  userSession )
{
   
    public Screens Run()
    {
        bool running = true;
        while (running)
        {

            PrintHeader();
            Console.WriteLine("[1] Registrieren");
            Console.WriteLine("[2] Anmelden");
            Console.WriteLine("[3] Beenden");
            App.PrintChooseOption();

            string eingabe = Console.ReadKey().KeyChar.ToString();

            switch (eingabe)
            {
                case "1":
                    RegistrationsMenu();
                    break;

                case "2":
                    return LoginMenu();

                case "3":
                    return Screens.ExitMenu;
                    break;


                default:
                    App.PrintWrongInput(); 
                    break;  
            }
        }
        return Screens.MainMenu;
    }


    private Screens RegistrationsMenu()
    {
        bool runningRegistration = true;
        while (runningRegistration)
        {

            Console.Clear();
            
            PrintHeader();
            Console.Write("Vorname: ");
            string firstName = Console.ReadLine() ?? "";
            
            PrintHeader();
            Console.Write("Nachname: ");
            string lastName = Console.ReadLine() ?? "";
            
            PrintHeader();
            Console.Write("Phone: ");
            string phone = Console.ReadLine() ?? "";
            
            
            bool runningEmailRegistration = true;
            string mailAddress = string.Empty;
            while (runningEmailRegistration)
            {
                PrintHeader();
                Console.Write("E-Mail Benutzername: ");
                string user = (Console.ReadLine() ?? "");
                
                PrintHeader();
                Console.Write("E-Mail Domain (z.B. gmail.com): ");
                string domain = (Console.ReadLine() ?? "");
                
                mailAddress = $"{user}@{domain}";


                try
                {
                    EmailServices.IsValid(mailAddress);

                    PrintHeader();
                    Console.Write("E-Mail Benutzername bestätigen: ");
                    string user2 = (Console.ReadLine() ?? "").Trim();

                    PrintHeader();
                    Console.Write("E-Mail Domain bestätigen: ");
                    string domain2 = (Console.ReadLine() ?? "").Trim();

                    string confirm = $"{user2}@{domain2}";
                    EmailServices.ConfirmRegistrationChecker(mailAddress, confirm);
                    runningEmailRegistration = false;
                }
                catch (WrongInputException ex)
                {
                    Console.WriteLine(ex.Message);

                    if (!PrintRetryMenu())
                    {
                        runningRegistration = false;
                        break;
                    }
                    

                }
            }

            if (runningRegistration)
            {


                bool runningUsernameValidation = true;
                string username = "";
                while (runningUsernameValidation)
                {
                    PrintHeader();
                    Console.Write("Username: ");
                    username = Console.ReadLine() ?? "";
                    try
                    {
                        service.isUsernameAvailable(username);
                        runningUsernameValidation = false;
                    }
                    catch (UsernameNotAvailableException e)
                    {
                        PrintHeader();
                        Console.WriteLine(e.Message);
                        PrintRetryMenu();
                        
                        if (!PrintRetryMenu())
                        {
                            return Screens.StartMenu;
                        }
                    }

                    PrintHeader();
                    Console.Write("Passwort: ");
                    string hash =
                        Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(Console.ReadLine() ?? "")));
                    service.Register(firstName, lastName, phone, hash,phone, mailAddress);
                    Console.WriteLine("Registrierung erfolgreich. Bitte melden dich an.");

                    Console.ReadKey();
                    return Screens.StartMenu;

                }

            }
        }
        return Screens.StartMenu;
        }

        private Screens LoginMenu()
        {

            
            bool running = true;
            while (running)
            {
                PrintHeader();
                Console.Write("Username: ");
                string username = Console.ReadLine() ?? "";
                Console.Write("Passwort: ");
                string hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(Console.ReadLine() ?? "")));
                if (service.LoginChecker(username, hash))
                {
                    userSession.CurrentUser   = service.GetCustomer(username, hash);
                    App.PrintSplitter();
                    Console.WriteLine("Anmeldung erfolgreich. Enter drücken um fortzufahren.");
                    Console.ReadKey();
                    return Screens.MainMenu;

                }
                
                PrintHeader();
                Console.WriteLine("Anmeldung fehlgeschlagen");
                if (!PrintRetryMenu())
                {
                    return Screens.StartMenu;
                }
            }

            return Screens.StartMenu;



        }
        
    

    public static void PrintHeader()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                
                Console.WriteLine("=== PIMP YOUR BLECH ===");
                App.PrintSplitter();
            }

    public bool PrintRetryMenu()
    {
        
       
            App.PrintSplitter();
            Console.WriteLine("[1] Erneut versuchen\n[2] Abbrechen");
            App.PrintChooseOption();
            while (true)
            {
            string input = Console.ReadKey().KeyChar.ToString();

            switch (input)
            {
                case "1":
                   return true;
                    

                case "2":
                    return false;
                
                
                    
                
                            
            }
                        
                        
        }


    
    }


}