using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Enums;
using PimpYourBlech_Contracts.Query;

namespace PimpYourBlech_ClassLibrary.Services.Products;

public interface IProductService
{
    List<ProductDto> GetProducts();
    Task<int> CreateProduct(CarDto car, string name, string brand, int quantity, decimal price, ProductType productType,
        string description);




    public Task RegisterEngine(int productId, int ps, int kw, string displacement, Gear gear, Fuel fuel);


    public Task RegisterRim(int productId, decimal diameter, decimal width);


    public Task RegisterLights(int productId, int lumen, bool isLED);


    public Task RegisterColor(int productId, string colorName);


   

     
    Task<ProductDto> GetProductByIdAsync(int id);
   
    Task DeleteProductAsync(ProductDto p);
     
    Task UpdateProductAsync(ProductDto p);

    List<ProductType> GetProductTypes();
     
    Task<List<ProductDto>> ProductListQueryAsync(ProductListQuery query);
}