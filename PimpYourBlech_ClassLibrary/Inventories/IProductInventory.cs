using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Inventories;

public interface IProductInventory
{
    public void InsertProduct(Product product);
    
    public List<Product> ListProducts();
    
    public void DeleteProduct(Product p);
    
    public void UpdateProduct(Product p);
    
    public List<Product> ListEngines();
    
    public List<Product> ListRims();
 
}