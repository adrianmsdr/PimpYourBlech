using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Enums;

namespace PimpYourBlech_ClassLibrary.Inventories;

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
}