namespace TestAutoKonfigurator;

public class Car
{
    public Car(string name, string dateProduction, string datePermit, string brand, string model, int ps, int quantity, double price)
    {
        Name = name;
        DateProduction = dateProduction;
        DatePermit = datePermit;
        Brand = brand;
        Model = model;
        PS = ps;
        Quantity = quantity;
        Price = price;
    }

    public string Name { get; set; }
    public string DateProduction { get; set; }
    
    public string DatePermit { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int PS {get; set;}
    
    public int Quantity {get; set;}
    
    public double Price {get; set;}

    public override string ToString()
    {
    return "Name: " + Name 
                    + "\nHersteller: " + Brand
                    + "\nModell: " + Model
                    + "\nBaujahr: " + DateProduction
                    + "\nErstzulassung: " +  DatePermit
                    + "\nPS: " + PS
                    + "\nQuantity: " +  Quantity
                    + "\nPrice" + Price;
    }
}
