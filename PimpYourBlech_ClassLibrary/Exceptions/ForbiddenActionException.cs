namespace PimpYourBlech_ClassLibrary.Exceptions;

public class ForbiddenActionException: Exception
{
    public ForbiddenActionException(String message) : base(message)
    {
        
    }
}