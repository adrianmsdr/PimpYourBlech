using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.ValueObjects;

namespace PimpYourBlech_ClassLibrary.Services.Carts;

public interface ICartService
{
    public Cart AddProduct(Product product);
}