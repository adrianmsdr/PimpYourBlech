using PimpYourBlech_ClassLibrary.Exceptions;
using PimpYourBlech_ClassLibrary.ValueObjects;

namespace PimpYourBlech_ClassLibrary.Services.CustomerCommunication;

public static class EmailServices
{
    public static bool IsValid(string email)
    {
      
            var addr = new EmailAddress(email);
            return true;
       

}

    public static bool ConfirmRegistrationChecker(String mailAddress, string confirm)
    {
        if (!mailAddress.Equals(confirm, StringComparison.OrdinalIgnoreCase))
        {
           throw new WrongInputException("E-Mail-Adressen stimmen nicht überein.");
           
        }
        return true;
    }

   
    
}
