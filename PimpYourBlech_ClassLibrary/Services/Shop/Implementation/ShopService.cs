using PimpYourBlech_ClassLibrary.Inventories;

namespace PimpYourBlech_ClassLibrary.Services.Shop.Implementation;

public class ShopService(IProductInventory productInventory):IShopService
{
    public double GetCartTotal(int userId)
    {
        throw new NotImplementedException();
    }
}