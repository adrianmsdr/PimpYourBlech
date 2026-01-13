using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Services.CustomerCommunication;

public interface IEmailService
{
    Task SendRegistrationEmailAsync(Customer customer);
    

    bool MailAdressChecker(string mailAddress, String confirm);
    
    public Task SendOrderReplyEmail(Customer customer,Order order);

    public Task SendConfigurationRequestEmail(Customer customer, Configuration config);
}