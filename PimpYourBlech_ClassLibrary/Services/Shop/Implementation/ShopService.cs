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

    public List<Product> SearchProducts(string searchString)
    {
        List<Product> products = new List<Product>();
        foreach (Product p in productInventory.ListProducts() )
        {
            if (p.Brand.Equals(searchString) ||
                p.ArticleNumber.Equals(searchString) ||
                p.Name.Equals(searchString))
            {
                products.Add(p);
            }
        }
        return products;
    }

    public double GetCartTotal(int userId)
    {
        throw new NotImplementedException();
    }
}