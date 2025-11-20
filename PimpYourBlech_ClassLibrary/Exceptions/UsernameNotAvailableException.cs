namespace PimpYourBlech_ClassLibrary.Exceptions;

public class UsernameNotAvailableException :Exception
{
    public UsernameNotAvailableException(string message) : base(message)
    {
    }
    
}