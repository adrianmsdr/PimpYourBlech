namespace TestAutoKonfigurator.Interfaces.Inventories;

public interface ICarInventory
{
    public void InsertCar(Car car);
    
    public List<Car> ListCars();
}