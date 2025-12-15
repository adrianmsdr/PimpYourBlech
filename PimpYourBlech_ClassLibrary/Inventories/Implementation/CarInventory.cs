using Microsoft.EntityFrameworkCore;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Persistence;

namespace PimpYourBlech_ClassLibrary.Inventories.Implementation;

public sealed class CarInventory(IDatabase database):ICarInventory
{
   // private readonly List<Car> _cars = database.LoadCars(); // Laden der Daten
    
    public void InsertCar(Car c)
    {
       
        database.Cars.Add(c);
        database.SaveChanges();
     //   _cars.Add(c);
    // database.CreateCar(c);
       // database.SaveCars(_cars);
    }
    
    public List<Car> ListCars()
    {
        return database.Cars.ToList();

        //  return _cars;
    }

    public List<Product> GetAvailableColor(int Id)
    {
        return database.Colors.
            Where(x => x.CarId == Id)
            .Include(x=>x.Product)
            .Select(x=>x.Product)
            .ToList();
            
    }

    public void DeleteCar(Car car)
    {
        database.Cars.Remove(car);
        database.SaveChanges();
    }
}