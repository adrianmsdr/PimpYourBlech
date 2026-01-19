using Microsoft.Extensions.Logging;
using PimpYourBlech_ClassLibrary.Exceptions;
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
    
     public async Task<ProductDto> CreateProduct(CarDto car, string name, string brand, string quantity, string price,
         ProductType productType, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new WrongInputException("Name darf nicht leer sein.");
        }

        if (string.IsNullOrWhiteSpace(brand))
        {
            throw new WrongInputException("Hersteller darf nicht leer sein.");
        }

        if (!int.TryParse(quantity, out var quantityValue) || quantityValue < 0)
        {
            throw new WrongInputException("Bestand muss eine positive Ganzzahl sein.");
      
        }

        if (!decimal.TryParse(price, out var priceValue) || priceValue < 0)
        {
            throw new WrongInputException("Preis muss eine positive Zahl sein.");
        }

        if (car == null)
        {
            throw new WrongInputException("Bitte registriere den Artikel auf ein vorhandenes Fahrzeug.");
        }

        if (!Enum.IsDefined(typeof(ProductType), productType))
        {
            throw new WrongInputException("Bitte wähle einen Produkttyp.");
        }
        

        ProductDto p = new ProductDto
        {
            CarId = car.Id,
            Name = name,
            Brand = brand,
            Quantity = quantityValue,
            Price = priceValue,
            ProductType = productType,
            Description = description
        };
        
        
        _logger.LogInformation(
            "Product instance created. Name={Name}, CarId={CarId}, Type={Type}",
            name,
            car.Id,
            productType
        );
        return p;
    }

    public async Task<int> RegisterEngine(ProductDto productDto, string ps, string kw, string displacement, Gear gear, Fuel fuel)
    {
       
        if (string.IsNullOrWhiteSpace(ps) || string.IsNullOrWhiteSpace(kw))
        {
            throw new WrongInputException("Bitte alle Motor-Details ausfüllen (PS, kW).");
        }

        if (!int.TryParse(ps, out var psValue) || psValue < 0)
        {
            throw new WrongInputException("PS muss eine positive Ganzzahl sein.");
        }

        if (!int.TryParse(kw, out var kwValue) || kwValue < 0)
        {
            throw new WrongInputException("KW muss eine positive Ganzzahl sein.");
        }

        if (string.IsNullOrWhiteSpace(displacement))
        {
            throw new WrongInputException("Hubraum darf nicht leer sein.");
        }
        
        Product p = new Product
        {
            CarId = productDto.CarId,
            Name = productDto.Name,
            Brand = productDto.Brand,
            Quantity = productDto.Quantity,
            Price = productDto.Price,
            ProductType = productDto.ProductType,
            Description = productDto.Description
        };

        int id = await _productInventory.InsertProductAsync(p);
        
        p.EngineDetail = new EngineDetail
        {
            ProductId = id,
            Ps = psValue,
            Kw = kwValue,
            Displacement = displacement,
            Gear = gear,
            Fuel = fuel
        };
        _logger.LogInformation("Engine registered for product {ProductId}.", id);
        await _productInventory.UpdateProductAsync(p);
        return id;
    }

    public async Task<int> RegisterRim(ProductDto productDto, string diameter, string width)
    {
        
        if (string.IsNullOrEmpty(diameter) || string.IsNullOrEmpty(width))
        {
            throw new WrongInputException("Bitte Durchmesser und Breite der Felge ausfüllen.");
        }

        if (!decimal.TryParse(diameter, out var diameterValue) || diameterValue < 0)
        {
            throw new WrongInputException("Durchmesser muss eine positive Zahl sein.");
        }

        if (!decimal.TryParse(width, out var widthValue) || widthValue < 0)
        {
            throw new WrongInputException("Breite muss eine positive Zahl sein.");
         
        }

        Product p = new Product
        {
            CarId = productDto.CarId,
            Name = productDto.Name,
            Brand = productDto.Brand,
            Quantity = productDto.Quantity,
            Price = productDto.Price,
            ProductType = productDto.ProductType,
            Description = productDto.Description
        };
            
        int id = await _productInventory.InsertProductAsync(p);
        p.RimDetail = new RimDetail
        {
            ProductId = id,
            DiameterInInch = diameterValue,
            WidthInInch = widthValue
        };
       await _productInventory.UpdateProductAsync(p);
        _logger.LogInformation("Rim registered for product {ProductId}.", id);
        return id;
    }

    public async Task<int> RegisterLights(ProductDto productDto, string lumen, bool isLED)
    {
        if (string.IsNullOrEmpty(lumen))
        {
            throw new WrongInputException("Bitte die Lumen-Anzahl für die Lichtanlage ausfüllen.");
        }

        if (!int.TryParse(lumen, out var lumenValue) || lumenValue < 0)
        {
            throw new WrongInputException("Lumen muss eine positive Ganzzahl sein.");
        }
        Product p = new Product
        {
            CarId = productDto.CarId,
            Name = productDto.Name,
            Brand = productDto.Brand,
            Quantity = productDto.Quantity,
            Price = productDto.Price,
            ProductType = productDto.ProductType,
            Description = productDto.Description
        };
        int id = await _productInventory.InsertProductAsync(p);

        p.LightsDetail = new LightsDetail
        {
            ProductId = id,
            Lumen = lumenValue,
            IsLed = isLED
        };
       await _productInventory.UpdateProductAsync(p);
        _logger.LogInformation("Light registered for product {ProductId}", id);
        return id;
    }

    public async Task<int> RegisterColor(ProductDto productDto, string colorName)
    {

        if (string.IsNullOrEmpty(colorName))
        {
            throw new WrongInputException("Bitte Vorschaunamen für die Farbe eingeben.");
        }
        Product p = new Product
        {
            CarId = productDto.CarId,
            Name = productDto.Name,
            Brand = productDto.Brand,
            Quantity = productDto.Quantity,
            Price = productDto.Price,
            ProductType = productDto.ProductType,
            Description = productDto.Description
        };
        int id = await _productInventory.InsertProductAsync(p);
        
        p.ColorDetail = new ColorDetail
        {
            ProductId = id,
            DisplayName = colorName,
        };
       await  _productInventory.UpdateProductAsync(p);
        _logger.LogInformation("Color registered for product {ProductId}.",id);
        return id;
    }

    public async Task<int> InsertProduct(ProductDto productDto)
    {
        Product p = new Product
        {
            CarId = productDto.CarId,
            Name = productDto.Name,
            Brand = productDto.Brand,
            Quantity = productDto.Quantity,
            Price = productDto.Price,
            ProductType = productDto.ProductType,
            Description = productDto.Description
        };
        int id = await _productInventory.InsertProductAsync(p);
        return id;
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