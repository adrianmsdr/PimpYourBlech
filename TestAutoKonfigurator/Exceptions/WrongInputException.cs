namespace TestAutoKonfigurator.Exceptions;

public class WrongInputException:Exception
{
    public WrongInputException(string message) : base(message)
    {
    }
}