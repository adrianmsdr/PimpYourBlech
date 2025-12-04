using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Inventories;

public interface ICarInventory
{
    public void InsertCar(Car c);
    
    public List<Car> ListCars();
    
    void DeleteCar(Car c);
}