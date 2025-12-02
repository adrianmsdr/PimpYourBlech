using System.Net;
using System.Net.Mail;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Exceptions;
using PimpYourBlech_ClassLibrary.ValueObjects;

namespace PimpYourBlech_ClassLibrary.Services.CustomerCommunication;

public class EmailService : IEmailService
{
    
    public void SendRegistrationEmail(Customer customer)
    {
        string subject = "Deine Registrierung bei PimpYourBlech";
        string message = "Hallo " + customer.FirstName + ",\n" +
                         "deine Registrierung bei PimpYourBlech war erfolgreich.\n" +
                         "Viel Spaß beim Konfigurieren deiner Lieblingsfahrzeuge.\n" +
                         "Schau gerne auch mal bei unserem Ersatzteileshop vorbei.\n" +
                         "Bei fragen schau gerne in die FAQ oder kontaktiere uns direkt per mail über unser Kundenportal" +
                         "und nun ist es Zeit dein Blech aufzupimpen 😉";

        using var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("pimpyourblech@gmail.com", "yswx nobp xhgk sjzv")
            }
            ;

        using var mail = new MailMessage(from: "pimpyourblech@gmail.com",
            to: customer.MailAddress, subject,message);
        client.Send(mail);
    }
    public static bool IsValid(string email)
    {
      
            var addr = new EmailAddress(email);
            return true;
       

}

    public static bool ConfirmRegistrationChecker(String mailAddress, string confirm)
    {
        if (!mailAddress.Equals(confirm, StringComparison.OrdinalIgnoreCase))
        {
           throw new WrongInputException("E-Mail-Adressen stimmen nicht überein.");
           
        }
        return true;
    }

   
    
}
