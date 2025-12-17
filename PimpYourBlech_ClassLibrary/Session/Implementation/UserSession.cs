using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Session;

namespace PimpYourBlech_ClassLibrary.Session.Implementation;

public class UserSession : IUserSession
{
    public Customer? CurrentUser { get; set; }

    public bool IsLoggedIn => CurrentUser != null;

    public bool IsAdmin { get; }

    public Configuration? CurrentConfiguration { get; set; }

    public DeliveryAddress? DeliveryAddress { get; set; }
    
    public PaymentValues? PaymentValues { get; set; }

    public void LogOut()
    {
        CurrentUser = null;
        DeliveryAddress = null;
    }

    public void LogIn(Customer customer)
    {
        CurrentUser = customer;
    }
}