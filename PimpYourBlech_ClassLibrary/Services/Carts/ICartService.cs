using PimpYourBlech_ClassLibrary.ValueObjects;
using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.Services.Carts;

public interface ICartService
{
    Cart AddProduct(Cart cart, ProductDto product);
    void RemoveProduct(Cart cart, CartPosition cp);


  public double GetTotalPrice(Cart cart);
}