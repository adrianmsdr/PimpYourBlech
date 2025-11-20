using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Inventories;

public interface IProductInventory
{
    public void InsertProduct(Product product);
    
    public List<Product> ListProducts();

    // _____________________________________ nur für die konsole erstmal __________________________________________

    public List<Product> ListEngines();
    
    public void InsertEngine(Engine engine);
}