using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.Services.Cars;

public interface ICarService
{
    // Gibt alle Produkte, die auf ein Auto registriert sind, aus
    Task<List<ProductDto>> GetRegisteredProductsAsync(int carId);
}