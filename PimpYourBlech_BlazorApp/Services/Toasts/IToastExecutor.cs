namespace PimpYourBlech_BlazorApp.Services.Toasts;

public interface IToastExecutor
{
    public Task<bool> RunAsync(
        Func<Task> action,
        string? successMessage = null,
        string? errorMessage = null,string? logError = null);

    public Task<T?> RunReturnAsync<T>(
        Func<Task<T>> action,
        string? successMessage = null,
        string? errorMessage = null,string? logError = null);
}