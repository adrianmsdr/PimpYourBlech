using PimpYourBlech_ClassLibrary.Session;

namespace PimpYourBlech_ClassLibrary.Entities;

public class Customer
    {
    public int Id { get; set; }
    
    public List<Order> Orders { get; set; } = new();
    public List<Configuration> Configurations { get; set; } = new ();
    public string? FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public List<DeliveryAddress> DeliveryAddresses { get; set; } = new();

    public string? Telefon { get; set; }

    public string? MailAddress { get; set; }


    public bool AdminRights { get; set; } 
    
    public string ImagePath { get; set; } = string.Empty;
    

    public override string ToString()
    {
        return "Vorname: " + FirstName + "\nNachname: " + LastName + "\nUsername: " + Username + "\nPasswort: " + PasswordHash + "\nTelefon: " + Telefon + "\nE-Mail: " + MailAddress  + "\nAdmin: " +  AdminRights;
    }

    }