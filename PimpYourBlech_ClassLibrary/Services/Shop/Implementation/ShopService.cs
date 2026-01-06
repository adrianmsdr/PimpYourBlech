using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Inventories;
using PimpYourBlech_ClassLibrary.Services.Carts;
using PimpYourBlech_ClassLibrary.ValueObjects;
using PimpYourBlech_ClassLibrary.Enums;

namespace PimpYourBlech_ClassLibrary.Services.Shop.Implementation;

public class ShopService(IProductInventory productInventory, ICartService cartService):IShopService
{
    private readonly IProductInventory _productInventory = productInventory;
    private readonly ICartService _cartService = cartService;
    public List<Product> GetProducts()
    {
        return productInventory.ListProducts().Where(p=>p.ProductType!=ProductType.Color).ToList();
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

    public List<Product> FilterProducts(string selectedCategory)
    {
        List<Product> filteredProducts = new List<Product>();
        foreach (Product p in productInventory.ListProducts())
        {
            switch (selectedCategory)
            {
                case "All":
                    filteredProducts.Add(p);
                    break;
                
                case "Engine":
                    if (p.EngineDetail != null)
                    {
                        filteredProducts.Add(p);
                    }

                    break;

                case "Rim":
                    if (p.RimDetail != null)
                    {
                        filteredProducts.Add(p);
                    }
                    break;

                case "Lights":
                    if (p.LightsDetail != null)
                    {
                        filteredProducts.Add(p);
                    }

                    break;
            }
        }
        return filteredProducts;
    }

    public double GetTotalPrice(int userId)
    {
        if (_cartService?.CartProductsList == null)
            return 0;

        double total = 0;

        foreach (var product in _cartService.CartProductsList)
        {
            total += product.Price;
        }

        return total;
    }

}