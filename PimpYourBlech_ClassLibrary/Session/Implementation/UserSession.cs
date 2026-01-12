using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Session;
using PimpYourBlech_ClassLibrary.ValueObjects;

namespace PimpYourBlech_ClassLibrary.Session.Implementation;

public class UserSession : IUserSession
{
    public int CurrentUserId { get; set; }

    public bool IsLoggedIn => CurrentUserId != 0;

    public bool IsAdmin { get; }

    public Configuration? CurrentConfiguration { get; set; }

    public void LogIn(int customerId)
    {
        CurrentUserId = customerId;
    }

    public Cart CurrentCart { get; set; } = new Cart();
    public async Task UpdateCurrentCartAsync(Cart cart)
    {
        Console.WriteLine("ToDo");
    }


    public int CurrentDeliveryAddressId { get; set; }
    public PaymentValues? PaymentValues { get; set; }

    public void LogOut()
    {
        CurrentUserId = 0;
    }

    public void LogIn(Customer customer)
    {
        CurrentUserId = customer.Id;
    }
}