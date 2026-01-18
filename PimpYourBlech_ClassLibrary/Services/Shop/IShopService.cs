using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Query;

namespace PimpYourBlech_ClassLibrary.Services.Shop;

public interface IShopService
{
    
    // Getter für alle Produkte die im Shop verfügbar sein sollen
    public Task<List<ProductDto>> GetProductsAsync(ProductListQuery q);
}