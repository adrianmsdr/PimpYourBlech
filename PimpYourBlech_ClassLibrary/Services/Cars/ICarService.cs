using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Query;

namespace PimpYourBlech_ClassLibrary.Services.Cars;

public interface ICarService
{
    // Gibt alle Produkte, die auf ein Auto registriert sind, aus
    Task<List<ProductDto>> GetRegisteredProductsAsync(int carId);
    
    Task<List<CarDto>> GetCarsAsync();
   
    Task<CarDto> RegisterCarAsync(string name, string dateProduction, string datePermit, string brand, string model,
        string ps,
        string quantity, string price);
   
    Task<CarDto> GetCarByIdAsync(int id);
   
    Task DeleteCarAsync(CarDto car);
   
    public Task UpdateCarAsync(CarDto car);
   
    public  Task<List<ProductDto>> GetAvailableRims(int id);
    public Task<List<ProductDto>> GetAvailableColors(int id);
   
    Task<List<CarDto>> CarListQueryAsync(CarListQuery q);
}