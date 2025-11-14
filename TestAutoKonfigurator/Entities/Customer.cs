using System.Net.Mail;

namespace TestAutoKonfigurator;

public class Customer (string firstName, string lastName, string username, string passwordHash, string telefon, string mailAddress)
    {
        
        private readonly List<Configuration> configurations;
        
    
    //Getter/Setter
    public string FirstName { get; set; } = firstName;

    public string LastName { get; set; } = lastName;

    public string Username { get; set; } = username;

    public string PasswordHash { get; set; } = passwordHash;


    public string Telefon { get; set; } = telefon;

    public string MailAddress { get; set; } = mailAddress;


    public bool AdminRights { get; set; } = false;
    

    public override string ToString()
    {
        return "Vorname: " + FirstName + "\nNachname: " + LastName + "\nUsername: " + Username + "\nPasswort: " + PasswordHash + "\nTelefon: " + Telefon + "\nE-Mail: " + MailAddress  + "\nAdmin: " +  AdminRights;
    }

    }