using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Enums;
using PimpYourBlech_Contracts.Query;

namespace PimpYourBlech_ClassLibrary.Services.Products;

public interface IProductService
{
    List<ProductDto> GetProducts();
    Task<ProductDto> CreateProduct(CarDto car, string name, string brand, string quantity, string price,
        ProductType productType,
        string description);




    public Task<int> RegisterEngine(ProductDto p, string ps,string  kw, string displacement, Gear gear, Fuel fuel);


    public Task<int> RegisterRim(ProductDto p, string diameter, string width);


    public Task<int> RegisterLights(ProductDto p, string lumen, bool isLED);


    public Task<int> RegisterColor(ProductDto p, string colorName);
    
    


   

     
    Task<ProductDto> GetProductByIdAsync(int id);
   
    Task DeleteProductAsync(ProductDto p);
     
    Task UpdateProductAsync(ProductDto p);

    List<ProductType> GetProductTypes();
     
    Task<List<ProductDto>> ProductListQueryAsync(ProductListQuery query);
}