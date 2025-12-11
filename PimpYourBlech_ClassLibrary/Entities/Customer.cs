namespace PimpYourBlech_ClassLibrary.Entities;

public class Customer ()
    {
        
        
    
    //Getter/Setter
    public int Id { get; set; }
    public List<Configuration> Configurations { get; set; } = new List<Configuration>();
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Username { get; set; }

    public string PasswordHash { get; set; }


    public string? Telefon { get; set; }

    public string? MailAddress { get; set; }


    public bool? AdminRights { get; set; } 
    

    public override string ToString()
    {
        return "Vorname: " + FirstName + "\nNachname: " + LastName + "\nUsername: " + Username + "\nPasswort: " + PasswordHash + "\nTelefon: " + Telefon + "\nE-Mail: " + MailAddress  + "\nAdmin: " +  AdminRights;
    }

    }