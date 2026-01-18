using PimpYourBlech_Contracts.Query;
using PimpYourBlech_Data.Models;

namespace PimpYourBlech_Data.Inventories;

public interface IProductInventory
{
    public Task InsertProductAsync(Product product);

    public List<Product> ListProducts();

    public Task DeleteProductAsync(Product p);

    public Task UpdateProductAsync(Product p);

    Task<List<Product>> ListEnginesAsync();

    Task<List<Product>> ListRimsAsync();

    Task<Product?> GetProductByIdAsync(int id);

    Task<List<Product>> QueryAsync(ProductListQuery q);

   Task<List<Product>> GetProductsForCarsAsync(int carId);
   
   Task SellProductsAsync(OrderPosition orderPosition);
   
}