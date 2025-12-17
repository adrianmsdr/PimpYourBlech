using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Session.Implementation;

namespace PimpYourBlech_ClassLibrary.Session;

using PimpYourBlech_ClassLibrary.Entities;

public interface IUserSession
{
    Customer? CurrentUser { get; set; }

    bool IsLoggedIn { get; }

    bool IsAdmin { get; }

    Configuration? CurrentConfiguration { get; set; }

    DeliveryAddress? DeliveryAddress { get; set; }
    
    PaymentValues? PaymentValues { get; set; }

    void LogOut();

    void LogIn(Customer customer);
}
