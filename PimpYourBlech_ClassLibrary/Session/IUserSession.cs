using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Session;

public interface IUserSession
{


    public Customer  CurrentUser { get; set; }
    
  public bool IsLoggedIn { get; }
    bool IsAdmin { get; }
    
    public Configuration CurrentConfiguration { get; set; }
    
    void LogOut();
    void LogIn(Customer customer);

    string ToString();
}