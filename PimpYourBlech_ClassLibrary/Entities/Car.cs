namespace PimpYourBlech_ClassLibrary.Entities;

public class Car
{

    public int Id { get; set; }
    public string Name { get; set; } 
    public string DateProduction { get; set; }
    
    public string DatePermit { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int PS { get; set; }
    
    public int Quantity {get; set;}
    
    public double Price {get; set;}

    public List<Product> Colors { get; set; } = new List<Product>();
  
    

    public override string ToString()
    {
    return "Name: " + Name 
                    + "\nHersteller: " + Brand
                    + "\nModell: " + Model
                    + "\nBaujahr: " + DateProduction
                    + "\nErstzulassung: " +  DatePermit
                    + "\nPS: " + PS
                    + "\nQuantity: " +  Quantity
                    + "\nPreis: " + Price;
    }
}
