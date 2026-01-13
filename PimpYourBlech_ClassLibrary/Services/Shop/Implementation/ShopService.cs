using PimpYourBlech_ClassLibrary.DTO;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Inventories;
using PimpYourBlech_ClassLibrary.Services.Carts;
using PimpYourBlech_ClassLibrary.ValueObjects;
using PimpYourBlech_ClassLibrary.Enums;

namespace PimpYourBlech_ClassLibrary.Services.Shop.Implementation;

public class ShopService(IProductInventory productInventory):IShopService
{

    

    public Task<List<Product>> GetProductsAsync(ProductListQuery q)
    {
        // optional: q.NameContains trimmen etc.
        return productInventory.QueryAsync(q);
    }

}