using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.ValueObjects;

namespace PimpYourBlech_ClassLibrary.Services.Carts;

public interface ICartService
{
    Cart GetCart();
    Cart AddProduct(Product product);
    void RemoveProduct(Product product);

    List<Product> CartProductsList { get; }
}