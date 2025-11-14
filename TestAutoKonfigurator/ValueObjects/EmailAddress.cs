using System.Net.Mail;
using TestAutoKonfigurator.Exceptions;

namespace TestAutoKonfigurator.ValueObjects;

public class EmailAddress
{
    private readonly MailAddress _mail;

        public string Address => _mail.Address;
        public string User => _mail.User;
        public string Domain => _mail.Host;

        public EmailAddress(string input)
        {
            try
            {
                _mail = new MailAddress(input);

                // Manuelle Prüfung, da MailAddress viele Ausnahmen macht: Enthält die Adresse wirklich ein "@" und eine Domain?
                if (string.IsNullOrWhiteSpace(_mail.Host))
                {
                    throw new WrongInputException("Ungültige E-Mail. Es fehlt die Domain. Beispiel: name@example.com");
                }
            }
            catch (Exception)
            {
                throw new WrongInputException("Ungültige E-Mail. Beispiel: name@example.com");
            }
            if (!_mail.Host.Contains("."))
            {
                throw new WrongInputException("Ungültige Domain. Beispiel: name@example.com");
            }
        }

        public override string ToString() => Address;

        public MailAddress ToMailAddress() => _mail;
    }