namespace TestAutoKonfigurator.Session.Implementation;

public class UserSession:IUserSession
{
    public Customer CurrentUser { get; set; }
    public bool IsAuthenticated { get; }
    public bool IsAdmin { get; }
}