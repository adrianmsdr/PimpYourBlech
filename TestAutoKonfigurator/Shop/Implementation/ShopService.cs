using TestAutoKonfigurator.Inventories;

namespace TestAutoKonfigurator.Shop.Implementation;

public class ShopService(IProductInventory productInventory):IShopService
{
    public double GetCartTotal(int userId)
    {
        throw new NotImplementedException();
    }
}