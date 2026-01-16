using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Query;
using PimpYourBlech_Data.Inventories;

namespace PimpYourBlech_ClassLibrary.Services.Shop.Implementation;

public class ShopService(IProductInventory productInventory) : IShopService
{
    public async Task<List<ProductDto>> GetProductsAsync(ProductListQuery q)
    {
        var products = await productInventory.QueryAsync(q);
        return products.ConvertAll(p => new ProductDto
        {
            ProductId = p.ProductId,
            CarId = p.CarId,
            Name = p.Name,
            ArticleNumber = p.ArticleNumber,
            Brand = p.Brand,

            Description = p.Description,
            Quantity = p.Quantity,
            Price = p.Price,

            ImageUrl = p.ImageUrl,
            ProductType = p.ProductType,
        });
    }
}