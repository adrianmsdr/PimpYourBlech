namespace TestAutoKonfigurator.Inventories;

public interface ICarInventory
{
    public void InsertCar(Car c);
    
    public List<Car> ListCars();
}