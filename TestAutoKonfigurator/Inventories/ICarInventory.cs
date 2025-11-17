namespace TestAutoKonfigurator.Inventories;

public interface ICarInventory
{
    public void InsertCar(string name,string dateProduction,string datePermit,string brand,string model, int ps,int quantity,double price);
    
    public List<Car> ListCars();
}