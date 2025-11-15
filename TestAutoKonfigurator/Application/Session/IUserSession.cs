using TestAutoKonfigurator.ValueObjects;

namespace TestAutoKonfigurator.Session;

public interface IUserSession
{

    public Customer  CurrentUser { get; set; }
    
    bool IsAuthenticated { get; }

    bool IsAdmin { get; }
}