using TestAutoKonfigurator.Enums;

namespace TestAutoKonfigurator.Inventories;

public interface IProductInventory
{
    public void InsertProduct(Product product);
    
    public List<Product> ListProducts();

    // _____________________________________ nur für die konsole erstmal __________________________________________

    public List<Product> ListEngines();
    
    public void InsertEngine(string name,
        string articleNumber,
        string brand,
        string description,
        int quantity,
        double price,
        int _ps,
        int _kw,
        string _displacement,
        Gear _gear);
}