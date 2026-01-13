using PimpYourBlech_ClassLibrary.DTO;
using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Services.Shop;

public interface IShopService
{
    

    //(Optional für später) Bestellen
    


    public Task<List<Product>> GetProductsAsync(ProductListQuery q);
}