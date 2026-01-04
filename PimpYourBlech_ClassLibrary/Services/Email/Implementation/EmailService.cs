using System.Net;
using System.Net.Mail;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Exceptions;
using PimpYourBlech_ClassLibrary.ValueObjects;

namespace PimpYourBlech_ClassLibrary.Services.CustomerCommunication.Implementation;

public class EmailService : IEmailService
{
    public void SendRegistrationEmail(Customer customer)
    {
        string subject = "Deine Registrierung bei PimpYourBlech";
        string message = "Hallo " + customer.FirstName + ",\n\n" +
            "dein Account bei PimpYourBlech wurde erfolgreich erstellt.\n" +
            "Du kannst dich jetzt einloggen und direkt loslegen.\n\n" +
            "Falls du dich nicht registriert hast: Dann war vermutlich jemand besonders begeistert von unserem System.\n" +
            "Ignorier die Mail einfach oder gib uns kurz Bescheid.\n\n" +
            "Bis gleich in der Werkstatt!";

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
    public bool IsValid(string email)
    {
      
            var addr = new EmailAddress(email);
            return true;
            
    }
    
    

    public bool ConfirmRegistrationChecker(String mailAddress, string confirm)
    {
        if (!mailAddress.Equals(confirm, StringComparison.OrdinalIgnoreCase))
        {
           throw new WrongInputException("E-Mail-Adressen stimmen nicht überein.");
           
        }
        return true;
    }

    public void SendOrderReplyEmail(Customer customer/*,Order order*/)
    {
        string subject = "Deine Bestellung bei PimpYourBlech";
        string message = "Hallo " + customer.FirstName + ",\n\n" +
                         "deine Bestellung bei PimpYourBlech war erfolgreich.\n" +
                         "Du kannst deine Bestellungen auch jederzeit in deinem Profil verwalten.\n\n" +
                         "Bestellübersicht:"/* +
                         order.ShowProducts()*/
            ;

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
}
