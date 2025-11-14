using System.Security.Cryptography;
using System.Text;
using TestAutoKonfigurator.Exceptions;
using TestAutoKonfigurator.Interfaces;

namespace TestAutoKonfigurator.Menus;

public class AdminMenu(
    ICustomerInventory customerInventory,
    IProductInventory productInventory,
    ICarInventory carInventory)
{

    // Start des Admin - Menüs (Hauptmenü)
    public void Start()
    {

        bool _running = true;
        while (_running)
        {
            PrintHeader();
            Console.WriteLine("[1] Kunden verwalten");
            Console.WriteLine("[2] Teile verwalten");
            Console.WriteLine("[3] Fahrzeuge verwalten");
            Console.WriteLine("[4] <-");
            Application.PrintChooseOption();
            string eigabe = Console.ReadKey().KeyChar.ToString();
            switch (eigabe)
            {
                case "1":
                    AdminCustomerMenu();
                    break;

                case "2":
                    AdminProductMenu();
                    break;

                /*case "3":
                    AdminCarMenu();
                    break;*/

                case "4":
                    _running = false;
                    break;
            }

        }


    }


    // Menü zum Verwalten von Kunden
    private void AdminCustomerMenu()
    {
        bool _running = true;

        while (_running)
        {

            PrintHeader();
            Console.WriteLine("[1] Kundenliste anzeigen");
            Console.WriteLine("[2] Kunden hinzufügen");
            Console.WriteLine("[3] Kunden suchen");
            Console.WriteLine("[4] Kundenliste löschen");
            Console.WriteLine("[5] <-");

            string eingabe = Console.ReadKey().KeyChar.ToString();

            switch (eingabe)
            {
                case "1":
                    ListCustomers();
                    break;

                case "2":
                    RegistrationsMenuAdmin();
                    break;

                case "3":
                    SearchCustomerMenu();
                    break;

                case "4":
                    DeleteCustomersMenu();
                    break;

                case "5":
                    _running = false;
                    break;
                
                default:
                    break;



            }
        }
    }


    // Menü zum Verwalten von Fahrzeugteilen
    private void AdminProductMenu()
    {
        bool _running = true;
        while (_running)
        {
            PrintHeader();
            Console.WriteLine("1) Fahrzeugteileliste anzeigen");
            Console.WriteLine("2) Fahrzeugteil hinzufügen");
            Console.WriteLine("3) Fahrzeugteil suchen");
            Console.WriteLine("4) Fahrzeugteileliste löschen");
            Application.PrintReturnMessage();

            string eingabe = Console.ReadLine() ?? "";

            switch (eingabe)
            {
                case "1":
                    ListProducts();
                    break;

                case "2":
                    AddProductMenu();
                    break;

                default:
                    _running = false;
                    break;
            }
        }
    }


   
   private void RegistrationsMenuAdmin()
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
                               registrationRetry = false;
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
       PrintHeader();
       Console.WriteLine("Wollen sie Administrator - Rechte zuweisen? [Y/N]");
       Application.PrintChooseOption();
       string response = Console.ReadKey().KeyChar.ToString();
       if (response == "Y" || response == "y")
       {
           c.AdminRights = true;
           PrintHeader();
           Console.WriteLine("Administrator - Rechte erfolgreich erteilt. Enter drücken um fortzufahren.");
           Console.ReadKey();
       }
       else if (response == "N" || response == "n")
       {
           c.AdminRights = false;
           PrintHeader();
           Console.WriteLine("Keine Administrator - Rechte erteilt. Enter drücken um fortzufahren.");
           Console.ReadKey();
       }
       
           customerInventory.InsertCustomer(c);
           PrintHeader();
           Console.WriteLine("Registrierung erfolgreich. Enter drücken um fortzufahren.");
           Console.ReadKey();
           running = false;
   }
      
   }

    // Hinzufügen von Produkten
    private void AddProductMenu() 
    
    {
        PrintHeader();
        Console.Write("Artikelnummer: ");
        string articleNumber = Console.ReadLine()!;
        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Hersteller: ");
        string brand = Console.ReadLine();
        Console.Write("Häufigkeit: ");
        int quantity = Convert.ToInt32(Console.ReadLine());
        Console.Write("Preis: ");
        double price = Convert.ToDouble(Console.ReadLine());
        Console.Write("Beschreibung: ");
        string description = Console.ReadLine();
        

        Product p = new Product();
        p.ArticleNumber = articleNumber;
        p.Name = name;
        p.Brand = brand;
        p.Quantity = quantity;
        p.Description = description;
        p.Price = price;
       
        
            productInventory.InsertProduct(p);

            Application.PrintReturnMessage();
        Console.ReadKey();
    }
    
    
    // Alle Kunden anzeigen lassen
    private void ListCustomers()
    {
        PrintHeader();
        foreach (Customer c in customerInventory.ListCustomers())
        {
            Console.WriteLine(c.ToString());
            Application.PrintSplitter();
        }

        Application.PrintReturnMessage();
        Console.ReadKey();
    }
    
    
    // Alle Produkte anzeigen lassen
    private void ListProducts()
    {
        PrintHeader();
        foreach (Product c in productInventory.ListProducts())
        {
            Console.WriteLine(c.ToString());
            Application.PrintSplitter();
        }

        Application.PrintReturnMessage();
        Console.ReadKey();
    }
    
    
    // Kunden suchen Menü
    private void SearchCustomerMenu()
    {
        bool runningSearch = true;

        while (runningSearch)
        {
            Console.Clear();
            Console.WriteLine("=== Administrator ===");
            Console.WriteLine("Kunden suchen über:");
            Console.WriteLine("[1] Username");
            Console.WriteLine("[2] Telefon");
            Console.WriteLine("[3] Vor- und Nachname");
            Console.WriteLine("[4] <-");
            string eingabe = Console.ReadKey().KeyChar.ToString();
            switch (eingabe)
            {
                case "1":
                    SearchCustomerSettings(eingabe,runningSearch);
                    break;

                case "2":
                    SearchCustomerSettings(eingabe, runningSearch);
                    break;
                    
                case "3":
                    SearchCustomerSettings(eingabe,runningSearch);
                   
                    break;
                   
                case "4":
                    runningSearch = false;
                    break;

                default:
                    break;


            }

        }
    }
    
    
    // Varianten zur Kundensuche (Username, Telefonnummer, Vor- und Zuname) (Um nicht 3 mal fast die selbe Methode schreiben zu müssen)
    private void SearchCustomerSettings(string eingabe, bool runningSearch)
    {
        string un = "";
        string te = "";
        string fn = "";
        string ln = "";
        
        PrintHeader();
        if (eingabe == "1")
        {
            Console.WriteLine("Username: ");
            un = Console.ReadLine();
        }
        else if (eingabe == "2")
        {
            Console.WriteLine("Telefon: ");
            te = Console.ReadLine();
        }
        else if (eingabe == "3")
        {
            Console.WriteLine("Vorname: ");
            fn = Console.ReadLine();
            Console.WriteLine("Nachname: ");
            ln = Console.ReadLine();
        }

        try
        {
            Customer c = new Customer("","","","","");
                if (eingabe == "1")
                {
                    c = customerInventory.GetCustomerByUsername(un);
                }
                else if (eingabe == "2")
                {
                    c = customerInventory.GetCustomerByTelefon(te);
                }
                else if (eingabe == "3")
                {
                    c = customerInventory.GetCustomerByNames(fn, ln);
                }

                PrintCustomerSettingsMenu(c);
                Application.PrintReturnMessage();
            

            string eingabe2 = Console.ReadLine() ?? "";
            switch (eingabe2)
            {
                case "1":
                    PrintCustomerDeleteMenu(c);
                    break;

                case "2":
                    UpdateCustomerMenu(c);

                    break;
                            
                case "3":
                    BannCustomerMenu(c);
                    break;

                default:
                    Application.PrintReturnMessage();
                    break;


            }
        }
        catch (NoCustomerFoundException e)
        {
            PrintNoCustomerFoundException(e,runningSearch);
        }
    }

    
    // Kunden sperren
    private void BannCustomerMenu(Customer customer)
    {
        
    }


    // Alle Kunden löschen
    private void DeleteCustomersMenu()
    {
       PrintHeader();
        Console.Write("Sind Sie sich sicher, dass Sie alle Kundendaten löschen wollen? [Y/N]: ");
       Application.PrintChooseOption();
        String eingabe = Console.ReadLine() ?? "";
        if (eingabe == "Y" || eingabe == "y")
        {
            customerInventory.DeleteCustomers();
            Console.WriteLine("Kundenliste erfolgreich gelöscht");
            Application.PrintReturnMessage();
            Console.ReadKey();
        }
        else if (eingabe == "N" || eingabe == "n")
        {
            Application.PrintReturnMessage();
            Console.ReadKey();
        }


    }
    
    
    // Kundendaten anpassen
    private void UpdateCustomerMenu(Customer customer)
    {

        PrintHeader();
        Console.WriteLine("Username: ");
        String username = Console.ReadLine()!;
        Console.WriteLine("Password: ");
        String password = Console.ReadLine()!;
        String hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
        Console.WriteLine("Telefon: ");
        String telefon = Console.ReadLine()!;
        
        Customer c = new Customer(customer.FirstName,customer.LastName,username, hash, telefon);
        
        PrintHeader();
        Console.WriteLine(c.ToString());
        Console.WriteLine("Daten übernehmen? [Y/N]: ");
        string eingabe = Console.ReadLine()!;
        if (eingabe == "Y" || eingabe == "y")
        {
            
            
            customerInventory.UpdateCustomer(customer,username, hash, telefon);
            
            Console.WriteLine("Erfolgreich überschrieben");
            Application.PrintReturnMessage();
            Console.ReadKey();
        }
        else if (eingabe == "N" || eingabe == "n")
        {
            Application.PrintReturnMessage();
            Console.ReadKey();
        }
    }
    
    
    
    // Bearbeitungsmenü eines Kunden
    private void PrintCustomerSettingsMenu(Customer c)
    {
        PrintHeader();
        Console.WriteLine(c.ToString());
        //Console.WriteLine("___________________________________");
        Application.PrintSplitter();
        Console.WriteLine("1) Benutzer löschen");
        Console.WriteLine("2) Benutzer bearbeiten");
        Console.WriteLine("3) Benutzer sperren");
        Application.PrintReturnMessage();
    }
    
    
    // Löschbestätigung
    private void PrintCustomerDeleteMenu(Customer c)
    {
        Console.WriteLine("Benutzer unwiderruflich löschen? [Y/N]");
        Application.PrintChooseOption();
        String d = Console.ReadLine();
        if (d == "Y" || d == "y")
        {

            customerInventory.DeleteCustomer(c);
            Console.WriteLine("Benutzer erfolgreich gelöscht");
            Application.PrintReturnMessage();
            Console.ReadKey();

        }
        else if (d == "N" || d == "n")
        {
            Console.WriteLine("Erfolgreich abgebrochen. ");
            Application.PrintReturnMessage();;
            Console.ReadKey();
        }
    }

    
    // Exception + beenden der aktellen Schleife durch bool Übergabe
    private void PrintNoCustomerFoundException(NoCustomerFoundException e,bool runningVar)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine("1) Erneut versuchen");
        Application.PrintReturnMessage();
        string eingabe2 = Console.ReadLine();
        switch (eingabe2)
        {
            case "1":
                break;

            default:

                Application.PrintReturnMessage();
                runningVar = false;
                break;

        }
    }

    
    // === Administrator=== printen
    public void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=== Administrator ===");
        Application.PrintSplitter();
         
    }

  

    
    
}