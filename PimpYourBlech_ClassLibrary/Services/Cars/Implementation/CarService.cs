using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Data.Inventories;

namespace PimpYourBlech_ClassLibrary.Services.Cars.Implementation;

public class CarService:ICarService
{
    private readonly ICarInventory _carInventory;
    private readonly IProductInventory _productInventory;

    public CarService(ICarInventory carInventory, IProductInventory productInventory)
    {
        _carInventory = carInventory;
        _productInventory = productInventory;
    }

    public async Task<List<ProductDto>> GetRegisteredProductsAsync(int carId)
    {
        var products = await _productInventory.GetProductsForCarsAsync(carId);
        return products.ConvertAll(p => new ProductDto
        {
            ProductId = p.ProductId,
            Name = p.Name,
            Brand = p.Brand,
            Price = p.Price,
            Description = p.Description,
            ArticleNumber = p.ArticleNumber,
            ImageUrl = p.ImageUrl,
            CarId = p.CarId,
            Quantity = p.Quantity,
            ProductType = p.ProductType,
        });
    }
}