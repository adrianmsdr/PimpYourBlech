using Blazored.Toast.Services;
using PimpYourBlech_ClassLibrary.Exceptions;

namespace PimpYourBlech_BlazorApp.Services.Toasts.Implementation;

public class ToastExecutor : IToastExecutor
{
    private readonly ILogger<ToastExecutor> _logger;
    private readonly IToastService _toast;

    public ToastExecutor(IToastService toast, ILogger<ToastExecutor> logger)
    {
        _toast = toast;
        _logger = logger;
    }

    // ohne Rückgabe
    public async Task<bool> RunAsync(
        Func<Task> action,
        string? successMessage = null,
        string? errorMessage = null, string? logError = null)
    {
        ArgumentNullException.ThrowIfNull(action); // Auszuführende Methode darf nicht null sein

        try
        {
            await action(); // auszuführende Methode wird ausgeführt

            if (!string.IsNullOrWhiteSpace(successMessage)) // bei Bedarf: Erfolgsanzeige
                _toast.ShowSuccess(successMessage);
            return true; // true = Task war erfolgreich
        }
        catch (Exception ex) when (
            ex is NoCustomerFoundException ||
            ex is UsernameNotAvailableException ||
            ex is ProductNotAvailableException ||
            ex is WrongInputException ||
            ex is WrongPasswordException) // eigene Exceptions werden gefangen
        {
            _toast.ShowError(errorMessage ?? ex.Message); // Toast - Benachrichtigung für den User in der GUI
            if (!string.IsNullOrWhiteSpace(logError)) // Bei Bedarf: Logging (als Warning)
            {
                _logger.LogWarning(
                    ex,
                    logError
                );
            }

            return false; // false = Task fehlgeschlagen
        }
        catch (Exception ex) when (
            ex is ForbiddenActionException) // eigene Exception für logisch verbotene Aktionen fangen
        {
            _toast.ShowError(errorMessage ?? ex.Message); // Toast - Benachrichtigung für den User in der GUI
            if (!string.IsNullOrWhiteSpace(logError)) // Bei Bedarf: Logging (als Error)
            {
                _logger.LogError(
                    ex,
                    logError
                );
            }

            return false; // false = Task fehlgeschlagen
            
        }
        catch (Exception ex) // sonstige Exception: User benachrichtigen und weiter werfen, um fehlerhafte Funktionen zu vermeiden
        {
            _toast.ShowError("Ein unerwarteter Fehler ist aufgetreten.");
            _logger.LogError(ex, ex.Message); // Logging mit Exception (als Error)
            throw;
        }
    }

    // Beliebiger Rückgabetyp
    public async Task<T?> RunReturnAsync<T>(
        Func<Task<T>> action,
        string? successMessage = null,
        string? errorMessage = null, string? logError = null)
    {
        ArgumentNullException.ThrowIfNull(action); // Auszuführende Methode darf nicht null sein

        try
        {
            var result = await action(); // auszuführende Methode wird ausgeführt (output wird in "result" gespeichert)

            if (!string.IsNullOrWhiteSpace(successMessage)) // Bei Bedarf: Erfolgsmeldung
                _toast.ShowSuccess(successMessage);

            return result; // Rückgabe des Ergebnisses
        }
        catch (Exception ex) when (
            ex is NoCustomerFoundException ||
            ex is UsernameNotAvailableException ||
            ex is WrongInputException ||
            ex is ProductNotAvailableException ||
            ex is WrongPasswordException) // eigene Exceptions werden gefangen
        {
            _toast.ShowError(errorMessage ?? ex.Message); // Toast - Benachrichtigung für den User in der GUI
            if (!string.IsNullOrWhiteSpace(logError)) //Bei Bedarf: Logging (als Warning)
            {
                _logger.LogWarning(
                    ex,
                    logError
                );
            }

            return default; // string = null, int = 0, ...
        }
        catch (Exception ex)// sonstige Exception: User benachrichtigen und weiter werfen, um fehlerhafte Funktionen zu vermeiden
        {
            _toast.ShowError("Ein unerwarteter Fehler ist aufgetreten,"); // Toast - Benachrichtigung für den User in der GUI
            _logger.LogError(ex, ex.Message); // Loggen (als Error)
            throw;
        }
    }
}