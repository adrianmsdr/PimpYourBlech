using System.Net;
using System.Net.Mail;
using System.Text;
using PimpYourBlech_ClassLibrary.Exceptions;
using PimpYourBlech_ClassLibrary.Services.Orders;
using PimpYourBlech_ClassLibrary.ValueObjects;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Data.Models;

namespace PimpYourBlech_ClassLibrary.Services.CustomerCommunication.Implementation;

public class EmailService : IEmailService
{
    // Versendet die Registrierungsbestätigung an einen Kunden
    public async Task SendRegistrationEmailAsync(string firstName, string mailAddress)
    {
        // Betreff + Inhalt der Registrierungs-Mail bauen
        string subject = "Deine Registrierung bei PimpYourBlech";
        string message = "Hallo " + firstName + ",\n\n" +
            "dein Account bei PimpYourBlech wurde erfolgreich erstellt.\n" +
            "Du kannst dich jetzt einloggen und direkt loslegen.\n\n" +
            "Falls du dich nicht registriert hast: Dann war vermutlich jemand besonders begeistert von unserem System.\n" +
            "Ignorier die Mail einfach oder gib uns kurz Bescheid.\n\n" +
            "Bis gleich in der Werkstatt!";

        // SMTP-Client konfigurieren (Gmail, SSL, Credentials)
        using var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("pimpyourblech@gmail.com", "yswx nobp xhgk sjzv")
            }
            ;

        // Mail erstellen und senden
        using var mail = new MailMessage(from: "pimpyourblech@gmail.com",
            to: mailAddress, subject,message);
       await client.SendMailAsync(mail);
    }
    
    // Prüft E-Mail auf Inhalt + Format + Übereinstimmung mit Bestätigung
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
    

    // Versendet eine Bestellbestätigung inkl. aller Bestellpositionen
    public async Task SendOrderReplyEmail(CustomerDto customer, List<OrderPositionDto> orderPositions)
    {
        // Bestellpositionen in einen Textblock bauen
       StringBuilder sb = new StringBuilder();
        foreach (var p in  orderPositions)
        {
            // Pro Position: Name, Artikelnummer, Marke, Preis*Anzahl
            sb.Append(p.Name + "\n\n" + p.ArticleNumber + "\n\n" + p.Brand + "\n\n" + p.UnitPrice*p.Quantity  + " €\n\n");
        }
        
        // Betreff + Inhalt der Bestellbestätigung bauen
        string subject = "Deine Bestellung bei PimpYourBlech";
        string message = "Hallo " + customer.FirstName + ",\n\n" +
                         "deine Bestellung bei PimpYourBlech war erfolgreich.\n" +
                         "Du kannst deine Bestellungen auch jederzeit in deinem Profil verwalten.\n\n" +
                         "Bestellübersicht: \n\n" + sb;

        // SMTP-Client konfigurieren
        using var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("pimpyourblech@gmail.com", "yswx nobp xhgk sjzv")
            }
            ;
        if (customer.MailAddress == null)
        {
            return;
        }
    
        // Mail erstellen und senden
        using var mail = new MailMessage(from: "pimpyourblech@gmail.com",
                to: customer.MailAddress, subject, message);
            await client.SendMailAsync(mail);
        
    }

    // Versendet eine Konfigurationsübersicht zu einer gespeicherten Konfiguration
    public async Task SendConfigurationRequestEmail(CustomerDto customer, ConfigurationDto config)
    {
        
    }
}
