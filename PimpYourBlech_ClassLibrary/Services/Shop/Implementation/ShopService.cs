using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Inventories;

namespace PimpYourBlech_ClassLibrary.Services.Shop.Implementation;

public class ShopService(IProductInventory productInventory):IShopService
{
    private readonly IProductInventory _productInventory = productInventory;
    public List<Product> GetProducts()
    {
        return productInventory.ListProducts();
    }
    
    public double GetCartTotal(int userId)
    {
        throw new NotImplementedException();
    }
}