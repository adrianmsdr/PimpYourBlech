using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Inventories;

public interface ICarInventory
{
    public Task InsertCarAsync(Car c);
    
    public Task<List<Car>> ListCarsAsync();
    
    Task DeleteCarAsync(Car c);
    
    public List<Product> GetAvailableColor(int Id);
    
    public Task UpdateCarAsync(Car car);
    
    public Task<Car> GetCarByIdAsync(int Id);
}