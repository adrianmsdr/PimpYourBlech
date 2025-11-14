using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using TestAutoKonfigurator.Exceptions;
using TestAutoKonfigurator.Interfaces;

namespace TestAutoKonfigurator.Menus;

public class StartMenu(ICustomerInventory customerInventory,IProductInventory productInventory,ICarInventory  carInventory )
{
    
    private readonly MainMenu _mainMenu = new (customerInventory, productInventory, carInventory);
   
    public void Start()
    {
        bool running = true;
        while (running)
        {

            PrintHeader();
            Console.WriteLine("[1] Registrieren");
            Console.WriteLine("[2] Anmelden");
            Console.WriteLine("[3] Beenden");
            Application.PrintChooseOption();

            string eingabe = Console.ReadKey().KeyChar.ToString();

            switch (eingabe)
            {
                case "1":
                    RegistrationsMenu();
                    break;

                case "2":
                    LoginMenu();
                    break;

                case "3":
                    running = false;
                    break;


                default:
                    Application.PrintWrongInput(); 
                    break;  
            }
        }
    }


    private void RegistrationsMenu()
    {
        bool running = true;
        while (running)
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
            
            bool runningAgain = true;
            string username = "";
            while (runningAgain)
            {
                PrintHeader();
                Console.Write("Username: ");
                username = Console.ReadLine() ?? "";
                try
                {
                    customerInventory.UsernameAcceptedChecker(username);
                    runningAgain = false;
                }
                catch (UsernameNotAvailableException e)
                {
                    PrintHeader();
                    Console.WriteLine(e.Message);
                    Console.ReadKey();

                    bool registrationRetry = true;
                    while (registrationRetry)
                    {
                        PrintHeader();
                        Console.WriteLine("[1] Erneut versuchen");
                        Console.WriteLine("[2] Abbrechen");
                        Application.PrintChooseOption();

                        string input = Console.ReadKey().KeyChar.ToString();

                        switch (input)
                        {
                            case "1":
                                registrationRetry = false;
                                break;

                            case "2":
                                running = false;
                                break;

                            default:
                                registrationRetry = true;
                                break;
                        }
                        
                        
                    }
                }

            }

                PrintHeader();
                Console.Write("Passwort: ");
                string hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(Console.ReadLine() ?? "")));
                Customer c = new Customer(firstName, lastName, username, hash, phone);
                customerInventory.InsertCustomer(c);
                Console.WriteLine("Registrierung erfolgreich. Enter drücken um fortzufahren.");
                Console.ReadKey();
                _mainMenu.Start(c.AdminRights);
        }
        }

        private void LoginMenu()
        {

            
            bool running = true;
            while (running)
            {
                PrintHeader();
                Console.Write("Username: ");
                string username = Console.ReadLine() ?? "";
                Console.Write("Passwort: ");
                string hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(Console.ReadLine() ?? "")));
                if (!customerInventory.LoginBlockedChecker(username, hash))
                {
                    customerInventory.GetCustomerAccount(username, hash);
                    Console.WriteLine("Anmeldung erfolgreich. Enter drücken um fortzufahren.");
                    Console.ReadKey();
                    _mainMenu.Start(customerInventory.GetCustomerAccount(username, hash).AdminRights);

                }
                else
                {
                    bool loginRetry = true;
                    while (loginRetry)
                    {

                        PrintHeader();
                        Console.WriteLine("Anmeldung fehlgeschlagen\n[1] Erneut versuchen\n[2] Abbrechen");
                        string input = Console.ReadKey().KeyChar.ToString();
                        switch (input)
                        {
                            case "1":
                                loginRetry = false;
                                break;

                            case "2":
                                loginRetry = false;
                                running = false;
                                break;

                            default:
                                loginRetry = true;
                                break;

                        }
                    }
                }
            }



        }

    private void RegisterCustomer(Customer c)
    {
            customerInventory.InsertCustomer(c);
            Console.WriteLine("Registrierung erfolgreich. Enter drücken um fortzufahren.");
            Console.ReadKey();

        }
    

    public static void PrintHeader()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                
                Console.WriteLine("=== PIMP YOUR BLECH ===");
                Application.PrintSplitter();
            }



}