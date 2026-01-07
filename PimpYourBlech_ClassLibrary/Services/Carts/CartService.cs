using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Services.Carts;
using PimpYourBlech_ClassLibrary.ValueObjects;


public class CartService : ICartService
{
   
    public Cart AddProduct(Cart cart,Product product)
    {
        if (product != null)
            cart.Products.Add(product);

        return cart;
    }

    public void RemoveProduct(Cart cart, Product product)
    {
        if (product != null)
            cart.Products.Remove(product);
    }
    
    public double GetTotalPrice(Cart cart)
    {

        double total = 0;

        foreach (var product in cart.Products)
        {
            total += product.Price;
        }

        return total;
    }
}