
using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.Services.CustomerCommunication;

public interface IEmailService
{
    Task SendRegistrationEmailAsync(CustomerDto customer);
    

    bool MailAdressChecker(string mailAddress, String confirm);
    
    public Task SendOrderReplyEmail(CustomerDto customer, List<OrderPositionDto> orderPositions);

    public Task SendConfigurationRequestEmail(CustomerDto customer, ConfigurationDto config);
}