using Blazored.Toast.Services;
using PimpYourBlech_ClassLibrary.Exceptions;

namespace PimpYourBlech_BlazorApp.Services.Toasts;

public class ToastExecutor : IToastExecutor
{
    private readonly ILogger<ToastExecutor> _logger;
    private readonly IToastService _toast;

    public ToastExecutor(IToastService toast, ILogger<ToastExecutor> logger)
    {
        _toast = toast;
        _logger = logger;
    }

    public async Task<bool> RunAsync(
        Func<Task> action,
        string? successMessage = null,
        string? errorMessage = null, string? logError = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        try
        {
            await action();

            if (!string.IsNullOrWhiteSpace(successMessage))
                _toast.ShowSuccess(successMessage);
            return true;
        }
        catch (Exception ex) when (
            ex is NoCustomerFoundException ||
            ex is UsernameNotAvailableException ||
            ex is WrongInputException ||
            ex is WrongPasswordException)
        {
            _toast.ShowError(errorMessage ?? ex.Message);
            if (!string.IsNullOrWhiteSpace(logError))
            {
                _logger.LogWarning(
                    ex,
                    logError
                );
            }

            return false;
        }
        catch (Exception ex)
        {
            _toast.ShowError("Ein unerwarteter Fehler ist aufgetreten.");
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<T?> RunReturnAsync<T>(
        Func<Task<T>> action,
        string? successMessage = null,
        string? errorMessage = null, string? logError = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        try
        {
            var result = await action();

            if (!string.IsNullOrWhiteSpace(successMessage))
                _toast.ShowSuccess(successMessage);

            return result;
        }
        catch (Exception ex) when (
            ex is NoCustomerFoundException ||
            ex is UsernameNotAvailableException ||
            ex is WrongInputException ||
            ex is WrongPasswordException)
        {
            _toast.ShowError(errorMessage ?? ex.Message);
            if (!string.IsNullOrWhiteSpace(logError))
            {
                _logger.LogWarning(
                    ex,
                    logError
                );
            }

            return default;
        }
        catch (Exception ex)
        {
            _toast.ShowError("Ein unerwarteter Fehler ist aufgetreten,");
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}