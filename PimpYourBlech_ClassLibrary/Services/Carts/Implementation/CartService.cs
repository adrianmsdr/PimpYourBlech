using PimpYourBlech_ClassLibrary.Exceptions;
using PimpYourBlech_ClassLibrary.ValueObjects;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Data.Inventories;

namespace PimpYourBlech_ClassLibrary.Services.Carts.Implementation;

public class CartService : ICartService
{
    private readonly IProductInventory _productInventory;

    public CartService(IProductInventory productInventory)
    {
        _productInventory = productInventory;
    }
    // Fügt ein Produkt dem Warenkorb hinzu
    public async Task AddProduct(Cart cart, ProductDto product)
    {
       var ExistingProduct = await _productInventory.GetProductByIdAsync(product.ProductId);
       if (ExistingProduct.Quantity == 0)
       {
           throw new ProductNotAvailableException("Nicht genügend Bestand");
       }
        // Sucht nach einer bereits bestehenden Warenkorbposition mit genau diesem Produkt
        CartPosition? cp = cart.Products.Find(x => x.ProductId == product.ProductId);

        // Wenn keine bestehende Warenkorbposition: neue Position erstellen
        if (cp == null)
        {
            cp = new CartPosition();
            cp.ProductId = product.ProductId;
            cp.Product = product;
            cp.Quantity = 1;
            cp.Price = product.Price;
            cart.Products.Add(cp);
        }
        // sonst: Häufigkeit der bestehenden Position erhöhen
        else
        {
            if (ExistingProduct.Quantity <= cp.Quantity)
            {
                throw new ProductNotAvailableException("Nicht genügend Bestand");

            }
            cp.Quantity++;
            cp.Price = product.Price * cp.Quantity;
        }


    }

    // Entfernt ein Produkt aus dem Warenkorb
    public void RemoveProduct(Cart cart, CartPosition? cartPosition)
    {
        if (cartPosition != null)
        {
            cart.Products.Remove(cartPosition);
        }
    }

    // Berechnet den Gesamtpreis des Warenkorbs
    public decimal GetTotalPrice(Cart cart)
    {
        decimal total = 0;

        foreach (var product in cart.Products)
        {
            total = total + product.Price;
        }

        return total;
    }
}