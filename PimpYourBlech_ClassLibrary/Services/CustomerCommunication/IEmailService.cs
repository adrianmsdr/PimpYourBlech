using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Services.CustomerCommunication;

public interface IEmailService
{
    void SendRegistrationEmail(Customer customer);
}