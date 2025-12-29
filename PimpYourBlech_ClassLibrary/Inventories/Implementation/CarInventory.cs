using Microsoft.EntityFrameworkCore;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Persistence;

namespace PimpYourBlech_ClassLibrary.Inventories.Implementation;

public sealed class CarInventory(IDatabase database):ICarInventory
{
    
    public void InsertCar(Car c)
    {
       
        database.Cars.Add(c);
        database.SaveChanges();
    
    }
    
    public List<Car> ListCars()
    {
        return database.Cars.ToList();

        //  return _cars;
    }

    public List<Product> GetAvailableColor(int Id)
    {
        return database.Products
            .Where(p => p.CarId == Id && p.ColorDetail != null)
            .Include(p => p.ColorDetail)
            .ToList();
            
    }

    public void DeleteCar(Car car)
    {
        database.Cars.Remove(car);
        database.SaveChanges();
    }

    public void UpdateCar(Car car)
    {
        database.Cars.Update(car);
        database.SaveChanges();
    }
}