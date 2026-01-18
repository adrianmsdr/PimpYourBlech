using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Query;

namespace PimpYourBlech_ClassLibrary.Services.Cars;

public interface ICarService
{
    // Getter für alle Produkte, die auf ein Fahrzeug registriert sind
    Task<List<ProductDto>> GetRegisteredProductsAsync(int carId);

    // Getter für alle registrierten Fahrzeuge
    Task<List<CarDto>> GetCarsAsync();

    // Validiert + registriert ein neues Fahrzeug
    Task<CarDto> RegisterCarAsync(string name, string dateProduction, string datePermit, string brand, string model,
        string ps,
        string quantity, string price);

    // Getter für ein Fahrzeug über seine ID
    Task<CarDto> GetCarByIdAsync(int id);

    // Löscht ein registriertes Fahrzeug
    Task DeleteCarAsync(CarDto car);

    // Updatet ein registriertes Fahrzeug
    public Task UpdateCarAsync(CarDto car);

    // Getter für alle verfügbaren Felgen eines Fahrzeugs
    public Task<List<ProductDto>> GetAvailableRims(int id);

    // Getter für alle verfügbaren Farben eines Fahrzeugs
    public Task<List<ProductDto>> GetAvailableColorsAsync(int id);

    // Query für Fahrzeuglisten (sortiert + gefiltert)
    Task<List<CarDto>> CarListQueryAsync(CarListQuery q);
}