

using PimpYourBlech_Contracts.Enums;
using PimpYourBlech_Contracts.Query;
using PimpYourBlech_Data.Models;

namespace PimpYourBlech_Data.Inventories;

public interface ICarInventory
{
    public Task InsertCarAsync(Car c);
    
    public Task<List<Car>> ListCarsAsync();
    public Task<List<Car>> ListCarsForConfigurationsAsync();
    
    Task DeleteCarAsync(Car c);
    
    public Task<List<Product>> GetAvailableColorsAsync(int Id);
    public Task<List<Product>> GetAvailableProductsAsync(int Id, ProductType type);
    
    public Task UpdateCarAsync(Car car);
    
    public Task<Car> GetCarByIdAsync(int Id);

    public Task<List<Product>> GetAvailableEnginesAsync(int carId);
    public Task<List<Product>> GetAvailableRimsAsync(int carId);

    public Task<List<Product>> GetAvailableExtrasAsync(int carId);
    Task<List<Car>> QueryAsync(CarListQuery q);
}