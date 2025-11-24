using Application.Menus;
using PimpYourBlech_ClassLibrary.Entities;

namespace TestAutoKonfigurator.Session;

public interface IUserSession
{
    public Screens CurrentScreen { get; set; }

    public Customer  CurrentUser { get; set; }
    
  public bool IsLoggedIn { get; }
    bool IsAdmin { get; }
    
    public Configuration CurrentConfiguration { get; set; }
    
    void LogOut();
    void LogIn(Customer customer);

    string ToString();
}