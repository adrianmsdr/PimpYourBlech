using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Inventories;

public interface IProductInventory
{
    public Task InsertProductAsync(Product product);

    public List<Product> ListProducts();

    public Task DeleteProductAsync(Product p);

    public Task UpdateProductAsync(Product p);
    
    public List<Product> ListEngines();
    
    public List<Product> ListRims();
    
    Task<Product?> GetProductByIdAsync(int id);
 
}