using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Services.Carts;
using PimpYourBlech_ClassLibrary.ValueObjects;

public class CartService : ICartService
{
    public Cart Cart { get; }

    public List<Product> CartProductsList => Cart.Products;

    public CartService()
    {
        Cart = new Cart();
    }

    public Cart GetCart() => Cart;

    public Cart AddProduct(Product product)
    {
        if (product != null)
            Cart.Products.Add(product);

        return Cart;
    }

    public void RemoveProduct(Product product)
    {
        if (product != null)
            Cart.Products.Remove(product);
    }
}