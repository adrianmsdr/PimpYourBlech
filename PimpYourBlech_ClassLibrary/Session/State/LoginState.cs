using PimpYourBlech_ClassLibrary.ValueObjects;

namespace PimpYourBlech_ClassLibrary.Session.State;

public static class LoginState
{
    public static int CurrentUserId { get; set; } = 0;
    
    public static Cart CurrentCart { get; set; }
}