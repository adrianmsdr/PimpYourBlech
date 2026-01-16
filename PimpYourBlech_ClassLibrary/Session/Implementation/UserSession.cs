using PimpYourBlech_ClassLibrary.Session;
using PimpYourBlech_ClassLibrary.ValueObjects;
using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.Session.Implementation;

public class UserSession : IUserSession
{
    public int CurrentUserId { get; set; }

    public bool IsLoggedIn => CurrentUserId != 0;

    public bool IsAdmin { get; }

    public ConfigurationDto? CurrentConfigurationDto { get; set; }

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
    public int CurrentPaymentValueId { get; set; }
    public PaymentValueDto? PaymentValues { get; set; }

    public void LogOut()
    {
        CurrentUserId = 0;
    }

    public void LogIn(CustomerDto customer)
    {
        CurrentUserId = customer.Id;
    }
}