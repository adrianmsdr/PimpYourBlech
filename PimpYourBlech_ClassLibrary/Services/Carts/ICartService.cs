using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.ValueObjects;

namespace PimpYourBlech_ClassLibrary.Services.Carts;

public interface ICartService
{
    Cart AddProduct(Cart cart, Product product);
    void RemoveProduct(Cart cart, CartPosition cp);


  public double GetTotalPrice(Cart cart);
}