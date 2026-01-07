using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Services.CustomerCommunication;

public interface IEmailService
{
    void SendRegistrationEmail(Customer customer);
    
    bool IsValid(string email);

    bool ConfirmRegistrationChecker(string mailAddress, String confirm);
    
    public Task SendOrderReplyEmail(Customer customer,Order order);

}