using TestAutoKonfigurator.Persistence;

namespace TestAutoKonfigurator.Inventories.Implementation;

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
}