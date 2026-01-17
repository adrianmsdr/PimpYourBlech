namespace PimpYourBlech_BlazorApp.Services.Toasts;


// Dieser Gui-Service ermöglicht es Methoden auszuführen und Exceptions gleich zu
// fangen und den User zu benachrichtigen
public interface IToastExecutor
{
    // Auszuführende Methode hat keinen Rückgabewert
    // Bool wird zurückgeben, um Erfolg/Fehler zu signalisieren
    public Task<bool> RunAsync(
        Func<Task> action,
        string? successMessage = null,
        string? errorMessage = null,string? logError = null);

    // Auszuführende Methode hat beliebigen Rückgabewert
    // Validierung auf Erfolg durch Prüfung des Rückgabewertes in der GUI
    public Task<T?> RunReturnAsync<T>(
        Func<Task<T>> action,
        string? successMessage = null,
        string? errorMessage = null,string? logError = null);
}