using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Query;

namespace PimpYourBlech_ClassLibrary.Services.Shop;

public interface IShopService
{
    

    //(Optional für später) Bestellen
    


    public Task<List<ProductDto>> GetProductsAsync(ProductListQuery q);
}