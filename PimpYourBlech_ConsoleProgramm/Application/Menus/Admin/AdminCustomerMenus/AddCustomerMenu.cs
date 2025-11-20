using System.Security.Cryptography;
using System.Text;
using Application.Menus;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Exceptions;
using PimpYourBlech_ClassLibrary.Services.Admin;
using PimpYourBlech_ClassLibrary.Services.CustomerCommunication;

namespace TestAutoKonfigurator.Application.Menus.Admin.AdminCustomerMenus;

public class AddCustomerMenu(
    IAdminService service)
{
    public Screens Run()
    {
       
        while (true)
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
            
            
            string mailAddress = string.Empty;
            while (true)
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
                    break;
                }
                catch (WrongInputException ex)
                {
                    Console.WriteLine(ex.Message);

                    if (!Retry())
                    {
                        return Screens.StartMenu;
                    }
                    

                }
            }
            
                string username = "";
                string hash = "";
                while (true)
                {
                    PrintHeader();
                    Console.Write("Username: ");
                    username = Console.ReadLine() ?? "";
                    try
                    {
                        service.isUsernameAvailable(username);
                    break;
                    }
                    catch (UsernameNotAvailableException e)
                    {
                        PrintHeader();
                        Console.WriteLine(e.Message);

                        if (!Retry())
                        {
                            return Screens.AdminCustomerMenu;
                        }

                    }
                }

                PrintHeader();
                Console.Write("Passwort: ");
                hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(Console.ReadLine() ?? "")));
                PrintHeader();
                Customer c = service.Register(firstName, lastName, username, hash, phone, mailAddress);
                while (true)
                {
                    PrintHeader();
                    Console.WriteLine("Wollen sie Administrator - Rechte zuweisen? [Y/N]");

                    App.PrintChooseOption();

                    string response = Console.ReadKey().KeyChar.ToString();

                    if (response == "Y" || response == "y")
                    {
                        c.AdminRights = true;
                        PrintHeader();
                        Console.WriteLine("Administrator - Rechte erfolgreich erteilt. Enter drücken um fortzufahren.");
                        Console.ReadKey();
                        PrintHeader();
                        Console.WriteLine("Registrierung erfolgreich. Enter drücken um fortzufahren.");
                        Console.ReadKey();

                        return Screens.AdminCustomerMenu;


                    }
                    else if (response == "N" || response == "n")
                    {
                        c.AdminRights = false;
                        PrintHeader();
                        Console.WriteLine("Keine Administrator - Rechte erteilt. Enter drücken um fortzufahren.");
                        Console.ReadKey();
                        PrintHeader();
                        Console.WriteLine("Registrierung erfolgreich. Enter drücken um fortzufahren.");
                        Console.ReadKey();

                        return Screens.AdminCustomerMenu;
                    }
                    else
                    {
                        App.PrintWrongInput();
                        Console.ReadKey();
                    }


                }
        }

        return Screens.AdminCustomerMenu;
    }
      
        public bool Retry()
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
      
        public void PrintHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("=== Kunden verwalten ===");
            App.PrintSplitter();

        }
}