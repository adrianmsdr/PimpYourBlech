namespace PimpYourBlech_Contracts.EntityDTOs;

public class CustomerDto
{
    public int Id { get; set; }
    
    public string? FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;


    public string? Telefon { get; set; }

    public string? MailAddress { get; set; }


    public bool AdminRights { get; set; } 
    
    public string ImagePath { get; set; } = string.Empty;
    

    public override string ToString()
    {
        return "Vorname: " + FirstName + "\nNachname: " + LastName + "\nUsername: " + Username + "\nPasswort: " + PasswordHash + "\nTelefon: " + Telefon + "\nE-Mail: " + MailAddress  + "\nAdmin: " +  AdminRights;
    }

}