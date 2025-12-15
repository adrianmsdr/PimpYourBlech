namespace PimpYourBlech_ClassLibrary.Entities;

public class ColorDetail
{
    public int Id { get; set; }
    
    public int CarId { get; set; }
    public Car? Car { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}