using TestAutoKonfigurator.Interfaces;

namespace TestAutoKonfigurator.Admin;

public sealed class CarInventory(IJsonDatabase database):ICarInventory
{
    private readonly List<Car> _cars = database.LoadCars(); // Laden der Daten
    
    public void InsertCar(Car c)
    {
        
        
        _cars.Add(c);
        database.SaveCars(_cars);
    }
    
    public List<Car> ListCars()
    {
        
        return _cars;
    }
}