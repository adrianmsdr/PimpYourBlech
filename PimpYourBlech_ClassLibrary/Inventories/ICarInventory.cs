using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Inventories;

public interface ICarInventory
{
    public Task InsertCarAsync(Car c);
    
    public List<Car> ListCars();
    
    Task DeleteCarAsync(Car c);
    
    public List<Product> GetAvailableColor(int Id);
    
    public Task UpdateCarAsync(Car car);
}