using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.ValueObjects;

namespace PimpYourBlech_ClassLibrary.Services.Carts;

public class CartService:ICartService
{
    private readonly Cart _cart;
    
    public Cart AddProduct(Product product)
    {
        _cart.Products.Add(product);
        return _cart;
    }
}