namespace PimpYourBlech_ClassLibrary.Exceptions;

public class WrongInputException:Exception
{
    public WrongInputException(string message) : base(message)
    {
    }
}