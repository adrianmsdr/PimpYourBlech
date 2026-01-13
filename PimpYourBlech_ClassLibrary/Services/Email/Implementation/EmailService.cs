using System.Net;
using System.Net.Mail;
using System.Text;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Exceptions;
using PimpYourBlech_ClassLibrary.ValueObjects;

namespace PimpYourBlech_ClassLibrary.Services.CustomerCommunication.Implementation;

public class EmailService : IEmailService
{
    public async Task SendRegistrationEmailAsync(Customer customer)
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
       await client.SendMailAsync(mail);
    }
    public bool MailAdressChecker(String mailAddress, string confirm)
    {
        if (string.IsNullOrWhiteSpace(mailAddress))
        {
            throw new WrongInputException("Email darf nicht leer sein.");
        }

      
        var addr = new EmailAddress(mailAddress);
       

        if (!mailAddress.Equals(confirm, StringComparison.OrdinalIgnoreCase))
        {
            throw new WrongInputException("E-Mail-Adressen stimmen nicht überein.");
           
        }
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

    public async Task SendOrderReplyEmail(Customer customer,Order order)
    {
       StringBuilder sb = new StringBuilder();
        foreach (var p in  order.Items)
        {
            sb.Append(p.Name + "\n\n" + p.ArticleNumber + "\n\n" + p.Brand + "\n\n" + p.UnitPrice*p.Quantity  + " €\n\n");
        }
        string subject = "Deine Bestellung bei PimpYourBlech";
        string message = "Hallo " + customer.FirstName + ",\n\n" +
                         "deine Bestellung bei PimpYourBlech war erfolgreich.\n" +
                         "Du kannst deine Bestellungen auch jederzeit in deinem Profil verwalten.\n\n" +
                         "Bestellübersicht: \n\n" + sb;

        using var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("pimpyourblech@gmail.com", "yswx nobp xhgk sjzv")
            }
            ;
        if (customer.MailAddress != null)
        {
            using var mail = new MailMessage(from: "pimpyourblech@gmail.com",
                to: customer.MailAddress, subject, message);
            await client.SendMailAsync(mail);
        }
    }

    public async Task SendConfigurationRequestEmail(Customer customer, Configuration config)
    {
        string subject = "Deine Konfigurationsübersicht (ID: " + config.Id + ")";
        string message = "Hallo " + customer.FirstName + ",\n\n" +
                         "deine Bestellung bei PimpYourBlech war erfolgreich.\n" +
                         "Du kannst deine Bestellungen auch jederzeit in deinem Profil verwalten.\n\n" +
                         "Bestellübersicht: Hierfür müsste eine Order jetzt mehrere Produkte speichern können, \\n\\n\" +" +
                         "nicht nur eins wie jetzt"
                         
            ;

        using var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("pimpyourblech@gmail.com", "yswx nobp xhgk sjzv")
            }
            ;
        if (customer.MailAddress != null)
        {
            using var mail = new MailMessage(from: "pimpyourblech@gmail.com",
                to: customer.MailAddress, subject, message);
            await client.SendMailAsync(mail);
        }
    }
}
