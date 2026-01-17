using Microsoft.Extensions.Logging;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Enums;
using PimpYourBlech_Contracts.Query;
using PimpYourBlech_Data.Inventories;
using PimpYourBlech_Data.Models;

namespace PimpYourBlech_ClassLibrary.Services.Products.Implememtation;

public class ProductService:IProductService
{
    private readonly IProductInventory _productInventory;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductInventory productInventory, ILogger<ProductService> logger)
    {
        _productInventory = productInventory;
        _logger = logger;
    }
    
     public async Task<int> CreateProduct(CarDto car, string name, string brand, int quantity, decimal price,
        ProductType productType, string description)
    {
        _logger.LogInformation(
            "Product instance created. Name={Name}, CarId={CarId}, Type={Type}",
            name,
            car.Id,
            productType
        );

        Product p = new Product
        {
            CarId = car.Id,
            Name = name,
            Brand = brand,
            Quantity = quantity,
            Price = price,
            ProductType = productType,
            Description = description
        };
        await InsertProduct(p);
        return p.ProductId;
    }

    public async Task RegisterEngine(int productId, int ps, int kw, string displacement, Gear gear, Fuel fuel)
    {
        var p = await _productInventory.GetProductByIdAsync(productId);
        
        p.EngineDetail = new EngineDetail
        {
            ProductId = p.ProductId,
            Ps = ps,
            Kw = kw,
            Displacement = displacement,
            Gear = gear,
            Fuel = fuel
        };
        _logger.LogInformation("Engine registered for product {ProductId}.", p.ProductId);
        await _productInventory.UpdateProductAsync(p);
    }

    public async Task RegisterRim(int productId, decimal diameter, decimal width)
    {
        var p = await _productInventory.GetProductByIdAsync(productId);

        p.RimDetail = new RimDetail
        {
            ProductId = p.ProductId,
            DiameterInInch = diameter,
            WidthInInch = width
        };
        _productInventory.UpdateProductAsync(p);
        _logger.LogInformation("Rim registered for product {ProductId}.", p.ProductId);
    }

    public async Task RegisterLights(int productId, int lumen, bool isLED)
    {
        var p = await _productInventory.GetProductByIdAsync(productId);

        p.LightsDetail = new LightsDetail
        {
            ProductId = p.ProductId,
            Lumen = lumen,
            IsLed = isLED
        };
        _productInventory.UpdateProductAsync(p);
        _logger.LogInformation("Light registered for product {ProductId}", p.ProductId);
    }

    public async Task RegisterColor(int productId, string colorName)
    {
        var p = await _productInventory.GetProductByIdAsync(productId);

        p.ColorDetail = new ColorDetail
        {
            ProductId = p.ProductId,
            DisplayName = colorName,
        };
        _productInventory.UpdateProductAsync(p);
        _logger.LogInformation("Color registered for product {ProductId}.", p.ProductId);
    }

    public async Task InsertProduct(Product p)
    {
        await _productInventory.InsertProductAsync(p); // muss SaveChangesAsync machen
        _logger.LogInformation($"Inserted product {p.ProductId}");
        
    }

    public List<ProductDto> GetProducts()
    {
        var products = _productInventory.ListProducts();
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
            ProductType = p.ProductType,
            Quantity = p.Quantity,
        });
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _productInventory.GetProductByIdAsync(id);
        return new ProductDto
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Brand = product.Brand,
            Price = product.Price,
            Description = product.Description,
            ArticleNumber = product.ArticleNumber,
            ImageUrl = product.ImageUrl,
            CarId = product.CarId,
            ProductType = product.ProductType,
            Quantity = product.Quantity,
        };
    }

    public async Task DeleteProductAsync(ProductDto p)
    {
        var product = await _productInventory.GetProductByIdAsync(p.ProductId);
        await _productInventory.DeleteProductAsync(product);
        _logger.LogWarning("Product deleted");
    }

    public async Task UpdateProductAsync(ProductDto p)
    {
        var product = await _productInventory.GetProductByIdAsync(p.ProductId);
        product.Name = p.Name;
        product.Brand = p.Brand;
        product.Price = p.Price;
        product.Description = p.Description;
        product.ArticleNumber = p.ArticleNumber;
        product.ImageUrl = p.ImageUrl;
        product.ProductType = p.ProductType;
        product.Quantity = p.Quantity;
        await _productInventory.UpdateProductAsync(product);
        _logger.LogInformation("Product updated");
    }

    public List<ProductType> GetProductTypes()
    {
        return Enum.GetValues(typeof(ProductType)).Cast<ProductType>().ToList();
    }

    public async Task<List<ProductDto>> ProductListQueryAsync(ProductListQuery query)
    {
       var products = await _productInventory.QueryAsync(query);
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
               ProductType = p.ProductType,
               Quantity = p.Quantity,
           }
       );
    }
    
}