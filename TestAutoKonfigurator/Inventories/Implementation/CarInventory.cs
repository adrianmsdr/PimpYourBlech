using TestAutoKonfigurator.Persistence;

namespace TestAutoKonfigurator.Inventories.Implementation;

public sealed class CarInventory(IDatabase database):ICarInventory
{
   // private readonly List<Car> _cars = database.LoadCars(); // Laden der Daten
    
    public void InsertCar(string name,string dateProduction,string datePermit,string brand,string model, int ps,int quantity,double price)
    {
        Car c = new Car();
        c.Name = name;
        c.DateProduction = dateProduction;
        c.DatePermit = datePermit;
        c.Brand = brand;
        c.Model = model;
        c.PS = ps;
        c.Quantity = quantity;
        c.Price = price;
        
     //   _cars.Add(c);
     database.CreateCar(c);
       // database.SaveCars(_cars);
    }
    
    public List<Car> ListCars()
    {
        return database.LoadCars();

        //  return _cars;
    }
}