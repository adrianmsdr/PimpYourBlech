using Application.Menus;
using PimpYourBlech_ClassLibrary.Entities;

namespace TestAutoKonfigurator.Session.Implementation;

public class UserSession:IUserSession
{
    public Screens CurrentScreen { get; set; }
    public Customer CurrentUser { get; set; }
    public bool IsLoggedIn => CurrentUser != null;
    public bool IsAdmin { get; }

    public Configuration CurrentConfiguration { get; set; }
    public void LogOut()
    {
        CurrentUser = null;
    }

    public void LogIn(Customer customer)
    {
        CurrentUser = customer;
    }

    public override string ToString()
    {
        return CurrentUser.ToString();
    }
}