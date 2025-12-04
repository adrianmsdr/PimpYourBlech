using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Inventories;

public interface IProductInventory
{
    public void InsertProduct(Product product);
    
    public List<Product> ListProducts();
    
    public void DeleteProduct(Product p);

    // _____________________________________ nur für die konsole erstmal __________________________________________

    public List<Product> ListEngines();
    
 
}