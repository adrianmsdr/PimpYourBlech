using System.ComponentModel.DataAnnotations;
using System.Reflection;
using PimpYourBlech_Contracts.DTOs;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Enums;
using PimpYourBlech_Data.Inventories;
using PimpYourBlech_Data.Models;

namespace PimpYourBlech_ClassLibrary.Services.Configurator.Implementation;

public class ConfiguratorService : IConfiguratorService
{

    private readonly ICustomerInventory _customerInventory;
    private readonly IProductInventory _productInventory;
    private readonly ICarInventory _carInventory;
    private readonly IConfigurationInventory _configurationInventory;

    public ConfiguratorService(
        ICustomerInventory customers,
        IProductInventory products,
        ICarInventory cars,
        IConfigurationInventory configurations
    )
    {
        _customerInventory = customers;
        _productInventory = products;
        _carInventory = cars;
        _configurationInventory = configurations;
    }
    public async Task<ConfigurationDto> StartNewConfiguration(CustomerDto customer, CarDto car, string name)
    {
        var config = new Configuration
        {
            Name = name,
            CarId = car.Id,
            CustomerId = customer.Id,
        };
        await _configurationInventory.AddConfigurationAsync(config);

        return new ConfigurationDto
        {
            Name = name,
            CarId = car.Id,
            CustomerId = customer.Id,
            Id = config.Id
        };
    }
    
    // Methode für die ausgabe des Fahrzeugvergleichs
    public async Task<List<ConfigurationDto>> GetAllConfigurationsForCustomerAsync(int customerId)
    {
        var configs =
            await _configurationInventory
                .GetConfigurationsIncludeCarProductsAsync(customerId);

        var result = new List<ConfigurationDto>();

        foreach (var c in configs)
        {
            var dto = new ConfigurationDto
            {
                Id = c.Id,
                Name = c.Name,
                CustomerId = c.CustomerId,
                CarId = c.CarId,

                Car = new CarDto
                {
                    Id = c.Car.Id,
                    Name = c.Car.Name,
                    Brand = c.Car.Brand,
                    Model = c.Car.Model,
                    PS = c.Car.PS,
                    Price = c.Car.Price,
                    DateProduction = c.Car.DateProduction,
                    DatePermit = c.Car.DatePermit,
                    Quantity = c.Car.Quantity
                },

                ProductCount = c.Products.Count,
                TotalPs = CalculateTotalPs(c)
            };

            dto.TotalPrice =
                c.Car.Price +
                c.Products.Sum(p => p.Price);

            result.Add(dto);
        }

        return result;
    }

    public async Task<CarDto> GetCarByIdAsync(int carId)
    {
        var car = await _carInventory.GetCarByIdAsync(carId);
        return car != null ? new CarDto
        {
            Id = car.Id,
            Brand = car.Brand,
            DatePermit = car.DatePermit,
            DateProduction = car.DateProduction,
            Price = car.Price,
            PS = car.PS,
            Model = car.Model,
            Name = car.Name,
            Quantity = car.Quantity,
        } : null;
        

    }
    
    public async Task AddProduct(int configurationId, int productId)
    {
        if (productId == 0 || configurationId == 0)
        {
            return;
        }

        var configuration = await GetConfigurationByIdAsync(configurationId);
        if (configuration == null)
        {

            return;
        }

        var product = await _productInventory.GetProductByIdAsync(productId);
        if (product == null)
        {

            var productDto = new ProductDto
            {
                ArticleNumber = product.ArticleNumber,
                Name = product.Name,
                CarId = product.CarId,
                Brand = product.Brand,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                ProductId = product.ProductId,
                ProductType = product.ProductType,
                Quantity = product.Quantity,
            };
        }
        var products = await GetRegisteredProductsAsync(configurationId);
        var existingProduct = products.FirstOrDefault(p => p.ProductType == product.ProductType);

        if (product.ProductType == ProductType.Lack
            || product.ProductType == ProductType.Felge
            || product.ProductType == ProductType.Motor)
        {
            if (existingProduct?.ProductId == product.ProductId)
                return;
            if (existingProduct != null)
            {
                await RemoveProduct(configurationId, existingProduct.ProductId);

            }
           
        }
        else
        {
            if (existingProduct?.ProductId == product.ProductId)
            {
                await RemoveProduct(configurationId, product.ProductId);
                return;
            }
        }
        await _configurationInventory.AddProduct(configurationId, product.ProductId);
        
        await _configurationInventory.UpdateConfigurationAsync(configuration.Id);  
    
    }
   
  /* public async Task AddProduct(int configurationId, int productId)
   {
       if (configurationId <= 0 || productId <= 0)
           return;

       var product = await _productInventory.GetProductByIdAsync(productId);
       if (product is null)
           return;

       var existing = await _configurationInventory.GetProductByType(configurationId, product.ProductType);

       bool exclusive = product.ProductType is
           ProductType.Lack or
           ProductType.Felge or
           ProductType.Motor;

       if (exclusive)
       {
           if (existing?.ProductId == productId)
               return;

           if (existing is not null)
               await _configurationInventory.RemoveProduct(configurationId, existing.ProductId);

           await _configurationInventory
               .AddProduct(configurationId, productId);
       }
       else
       {
           if (existing?.ProductId == productId)
           {
               await _configurationInventory
                   .RemoveProduct(configurationId, productId);
               return;
           }

           await _configurationInventory
               .AddProduct(configurationId, productId);
       }
   }*/

    //Preisberechnung
    public async  Task<decimal> CalculateTotalPriceAsync(ConfigurationDto configuration)
    {
        var config = await _configurationInventory.GetConfigurationByIdAsync(configuration.Id);
        var car = await _carInventory.GetCarByIdAsync(configuration.CarId);
        decimal carPrice = car.Price;
        var products = await _configurationInventory.GetAllProductsAsync(configuration.Id);
        decimal productTotal = products.Sum(p => p.Price);
        
        return carPrice + productTotal;
    }
    
    // PS Berechnung
    private int CalculateTotalPs(Configuration config)
    {
        var basePs = config.Car.PS;
        
        var motor = config.Products
            .FirstOrDefault(p => p.ProductType == ProductType.Motor);
        
        if (motor?.EngineDetail == null)
            return basePs;
        
        return motor.EngineDetail.Ps;
    }

    public async Task SaveConfigurationAsync(ConfigurationDto configuration, CustomerDto customer)
    {
        if (configuration.Id == 0)
        {
            Console.WriteLine("No configuration found. new initialiozed");

            var config = new Configuration
            {
                Id = configuration.Id,
                Name = configuration.Name,
                CarId = configuration.CarId,
                CustomerId = customer.Id,
            };
            await _configurationInventory.AddConfigurationAsync(config);
        }
        else
        {
            var existing = await _configurationInventory.GetConfigurationByIdAsync(configuration.Id);
            if (existing is not null)
            {
                existing.Name = configuration.Name;
               await _configurationInventory.UpdateConfigurationAsync(configuration.Id);
            }
        }
        
    }
    public async Task DeleteConfiguration(ConfigurationDto configuration)
    {
           await _configurationInventory.DeleteConfigurationAsync(configuration.Id);
    }

    public async Task RemoveProduct(int configurationId, int productId)
    {
        await _configurationInventory.RemoveProductAsync(configurationId, productId);
    }
    public async Task<List<ConfigurationDto>> GetAllConfigurations(CustomerDto customer)
    {
       var configs =  await _configurationInventory.GetConfigurationsForCustomerAsync(customer.Id);
       return configs.ConvertAll(p => new ConfigurationDto
       {
           Name = p.Name,
           CarId = p.CarId,
           CustomerId = p.CustomerId,
           Id = p.Id
       });
    }

   

    

    public async Task<List<ProductDto>> ListEnginesAsync()
    {
        var engines = await _productInventory.ListEnginesAsync();
        return engines.ConvertAll(p => new ProductDto
        {
            ArticleNumber = p.ArticleNumber,
            Name = p.Name,
            CarId = p.CarId,
            Brand = p.Brand,
            Description = p.Description,
            ImageUrl = p.ImageUrl,
            Price = p.Price,
            ProductId = p.ProductId,
            ProductType = p.ProductType,
            Quantity = p.Quantity,
        });
    }

    public async Task<List<ProductDto>> ListRimsAsync()
    {
        var rims = await _productInventory.ListEnginesAsync();
        return rims.ConvertAll(p => new ProductDto
        {
            ArticleNumber = p.ArticleNumber,
            Name = p.Name,
            CarId = p.CarId,
            Brand = p.Brand,
            Description = p.Description,
            ImageUrl = p.ImageUrl,
            Price = p.Price,
            ProductId = p.ProductId,
            ProductType = p.ProductType,
            Quantity = p.Quantity,
        });
    }

    public async Task<List<CarDto>> ListCarsAsync()
    {
        var cars = await _carInventory.ListCarsAsync();
        return cars.ConvertAll(p => new CarDto
            {
                Name = p.Name,
                Quantity = p.Quantity,
                Brand = p.Brand,
                Model = p.Model,
                DatePermit = p.DatePermit,
                DateProduction = p.DateProduction,
                Price = p.Price,
                PS = p.PS,
                Id = p.Id,
            }
        );
    }

    public async Task<ConfigurationDto> GetConfigurationByIdAsync(int configurationId)
    {
        var config = await _configurationInventory.GetConfigurationByIdAsync(configurationId);
        return config != null ? new ConfigurationDto
        {
            Name = config.Name,
            CarId = config.CarId,
            CustomerId = config.CustomerId,
            Id = config.Id,
        } : null;
    }

    public async Task<List<ProductDto>> GetAvailableColorsAsync(int Id)
    {
        var colors = await _carInventory.GetAvailableColorsAsync(Id);
        return colors.ConvertAll(p => new ProductDto
        {
            ArticleNumber = p.ArticleNumber,
            Name = p.Name,
            CarId = p.CarId,
            Brand = p.Brand,
            Description = p.Description,
            ImageUrl = p.ImageUrl,
            Price = p.Price,
            ProductId = p.ProductId,
            ProductType = p.ProductType,
            Quantity = p.Quantity,
        });
    }

    public async Task<List<ProductDto>> GetAvailableEnginesAsync(int carId)
    {
        var engines = await _carInventory.GetAvailableEnginesAsync(carId);
        return engines.ConvertAll(p => new ProductDto
            {
                ArticleNumber = p.ArticleNumber,
                Name = p.Name,
                CarId = p.CarId,
                Brand = p.Brand,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                ProductId = p.ProductId,
                ProductType = p.ProductType,
                Quantity = p.Quantity,
                Ps = p.EngineDetail.Ps,
                Kw = p.EngineDetail.Kw,
                Gear = p.EngineDetail.Gear,
                Fuel = p.EngineDetail.Fuel,
                Displacement = p.EngineDetail.Displacement,
            }
        );
    }

    public async Task<List<ProductDto>> GetAvailableRims(int carId)
    {
        var engines = await _carInventory.GetAvailableEnginesAsync(carId);
        return engines.ConvertAll(p => new ProductDto
            {
                ArticleNumber = p.ArticleNumber,
                Name = p.Name,
                CarId = p.CarId,
                Brand = p.Brand,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                ProductId = p.ProductId,
                ProductType = p.ProductType,
                Quantity = p.Quantity,
            }
        );
    }
    
    
    public string GetGearDisplayName(Gear gear)
    {
        var field = gear.GetType().GetField(gear.ToString());
        return field?
            .GetCustomAttribute<DisplayAttribute>()?
            .Name ?? gear.ToString();
    }

    public async Task<List<ProductDto>> GetAvailableExtras(int carId)
    {
    var extras = await _carInventory.GetAvailableExtrasAsync(carId);
    return extras.ConvertAll(p => new ProductDto
        {
            ArticleNumber = p.ArticleNumber,
            Name = p.Name,
            CarId = p.CarId,
            Brand = p.Brand,
            Description = p.Description,
            ImageUrl = p.ImageUrl,
            Price = p.Price,
            ProductId = p.ProductId,
            ProductType = p.ProductType,
            Quantity = p.Quantity,
        }
    );
    }

    public async Task<List<ProductDto>> GetAvailableProductsAsync(int carId, ProductType type)
    {
       var products =  await _carInventory.GetAvailableProductsAsync(carId, type);
       return products.ConvertAll(p => new ProductDto
           {
               ArticleNumber = p.ArticleNumber,
               Name = p.Name,
               CarId = p.CarId,
               Brand = p.Brand,
               Description = p.Description,
               ImageUrl = p.ImageUrl,
               Price = p.Price,
               ProductId = p.ProductId,
               ProductType = p.ProductType,
               Quantity = p.Quantity,
           }
       );
    }

    public async Task<bool> ConfigurationAvailable(int carId)
    {
        return !(await _carInventory.GetAvailableProductsAsync(carId, ProductType.Lack)).Any() ||
               !(await _carInventory.GetAvailableProductsAsync(carId, ProductType.Motor)).Any() ||
               !(await _carInventory.GetAvailableProductsAsync(carId, ProductType.Felge)).Any()
               ;
    }

    public async Task<List<CarDto>> GetAvailableCarsAsync()
    {
        var cars = await _carInventory.ListCarsForConfigurationsAsync();
        return cars.ConvertAll(c => new CarDto
        {
            Name = c.Name,
            Quantity = c.Quantity,
            Brand = c.Brand,
            Model = c.Model,
            DatePermit = c.DatePermit,
            DateProduction = c.DateProduction,
            Price = c.Price,
            PS = c.PS,
            Id = c.Id,
        });
    }

    public Task<List<ConfigurationCarItemsDto>> GetConfigurationBasicsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<ConfigurationCarItemsDto>> GetConfigurationsBasicsCustomerAsync(int customerId)
    {
        var configurations = await _configurationInventory.GetConfigurationsIncludeCarProductsAsync(customerId);
        return configurations.ConvertAll(c => new ConfigurationCarItemsDto
        {
            Name = c.Name,
            CarId = c.CarId,
            CarName = c.Car.Name,
            ConfigurationId = c.Id,
            ProductCount = c.Products.Count,

        });
    }

    public async Task<List<ProductDto>> GetRegisteredProductsAsync(int configId)
    {
        var config = await _configurationInventory.GetAllProductsAsync(configId);
        return config.ConvertAll(p => new ProductDto
        {
            ArticleNumber = p.ArticleNumber,
            Name = p.Name,
            CarId = p.CarId,
            Brand = p.Brand,
            Description = p.Description,
            ImageUrl = p.ImageUrl,
            Price = p.Price,
            ProductId = p.ProductId,
            ProductType = p.ProductType,
            Quantity = p.Quantity,
        });
    }
}