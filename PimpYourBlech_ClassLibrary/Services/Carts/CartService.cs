using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Services.Carts;
using PimpYourBlech_ClassLibrary.ValueObjects;


public class CartService : ICartService
{
   
    public Cart AddProduct(Cart cart,Product product)
    {
        CartPosition? cp = cart.Products.Find(x => x.ProductId == product.ProductId);
        if (cp == null)
        {
            cp = new CartPosition();
            cp.ProductId = product.ProductId;
            cp.Product = product;
            cp.Quantity = 1;
            cp.Price = (decimal)product.Price;
            cart.Products.Add(cp);
        }
        else
        {
            cp.Quantity++;
            cp.Price = (decimal)product.Price*cp.Quantity;
        }
        return cart;

    }

    public void RemoveProduct(Cart cart, CartPosition? cartPosition)
    {
        if(cartPosition != null){
            cart.Products.Remove(cartPosition);
        }
    }
    
    public double GetTotalPrice(Cart cart)
    {

        double total = 0;

        foreach (var product in cart.Products)
        {
            total = total + (double)product.Price;
        }

        return total;
    }
}