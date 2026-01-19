using PimpYourBlech_ClassLibrary.Session.Implementation;
using PimpYourBlech_ClassLibrary.ValueObjects;
using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.Session;


public interface IUserSession
{
    public int CurrentUserId { get; set; }

    bool IsLoggedIn { get; }

    bool IsAdmin { get; set; }

    ConfigurationDto? CurrentConfigurationDto { get; set; }
    

    int CurrentDeliveryAddressId { get; set; }
    int CurrentPaymentValueId { get; set; }
    PaymentValueDto? PaymentValues { get; set; }

    void LogOut();

    void LogIn(CustomerDto customer);
    void LogIn(int customerId);

    public Cart CurrentCart { get; set; } 
    
    Task UpdateCurrentCartAsync(Cart cart); //Adrian

}
