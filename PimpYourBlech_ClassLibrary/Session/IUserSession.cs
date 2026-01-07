using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Session.Implementation;
using PimpYourBlech_ClassLibrary.ValueObjects;

namespace PimpYourBlech_ClassLibrary.Session;

using PimpYourBlech_ClassLibrary.Entities;

public interface IUserSession
{
    Customer? CurrentUser { get; set; }

    bool IsLoggedIn { get; }

    bool IsAdmin { get; }

    Configuration? CurrentConfiguration { get; set; }

    
    PaymentValues? PaymentValues { get; set; }

    void LogOut();

    void LogIn(Customer customer);

    public Cart CurrentCart { get; set; } 

}
