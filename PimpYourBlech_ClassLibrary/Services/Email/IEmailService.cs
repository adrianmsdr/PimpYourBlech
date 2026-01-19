
using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.Services.CustomerCommunication;

public interface IEmailService
{
    // Versendet die Registrierungsbestätigung an einen Kunden
    Task SendRegistrationEmailAsync(string firstName, string mailAddress);
    
    // Prüft, ob E-Mail-Adresse und Bestätigung übereinstimmen und gültig sind
    bool MailAdressChecker(string mailAddress, String confirm);
    
    // Versendet eine Bestellbestätigung inkl. aller Bestellpositionen
    public Task SendOrderReplyEmail(CustomerDto customer, List<OrderPositionDto> orderPositions);

    // Versendet eine Anfrage/Übersicht zu einer Fahrzeugkonfiguration
    public Task SendConfigurationRequestEmail(CustomerDto customer, ConfigurationDto config);
}