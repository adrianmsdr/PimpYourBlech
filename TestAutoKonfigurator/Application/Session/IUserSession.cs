using Application.Menus;
using TestAutoKonfigurator.ValueObjects;

namespace TestAutoKonfigurator.Session;

public interface IUserSession
{
    public Screens CurrentScreen { get; set; }

    public Customer  CurrentUser { get; set; }
    
    bool IsAuthenticated { get; }

    bool IsAdmin { get; }
    
    public Configuration CurrentConfiguration { get; set; }
    
    void LogOut();
    void LogIn(Customer customer);

    string ToString();
}