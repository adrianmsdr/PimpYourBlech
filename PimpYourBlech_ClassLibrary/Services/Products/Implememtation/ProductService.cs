using PimpYourBlech_Data.Inventories;

namespace PimpYourBlech_ClassLibrary.Services.Products.Implememtation;

public class ProductService:IProductService
{
    private readonly IProductInventory _productInventory;

    public ProductService(IProductInventory productInventory)
    {
        _productInventory = productInventory;
    }
    
    
}