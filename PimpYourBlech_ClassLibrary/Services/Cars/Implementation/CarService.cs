using Microsoft.Extensions.Logging;
using PimpYourBlech_ClassLibrary.Exceptions;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Enums;
using PimpYourBlech_Contracts.Query;
using PimpYourBlech_Data.Inventories;
using PimpYourBlech_Data.Models;

namespace PimpYourBlech_ClassLibrary.Services.Cars.Implementation;

public class CarService:ICarService
{
    private readonly ICarInventory _carInventory;
    private readonly IProductInventory _productInventory;
    private readonly ILogger<CarService> _logger;

    public CarService(ICarInventory carInventory, IProductInventory productInventory, ILogger<CarService> logger)
    {
        _carInventory = carInventory;
        _productInventory = productInventory;
        _logger = logger;
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
    
     public async Task<List<CarDto>> GetCarsAsync()
    {
        
      var cars = await _carInventory.ListCarsAsync();
      return cars.ConvertAll(c => new CarDto
      {
          Brand = c.Brand,
          DatePermit = c.DatePermit,
          DateProduction = c.DateProduction,
          Price = c.Price,
          PS = c.PS,
          Model = c.Model,
          Id = c.Id,
          Name = c.Name,
          Quantity = c.Quantity,
      });
    }

    public async Task<CarDto> RegisterCarAsync(string name, string dateProduction, string datePermit, string brand,
        string model, string ps, string quantity,string price)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(model) || string.IsNullOrWhiteSpace(brand))
            throw new WrongInputException("Name, Hersteller und Modell dürfen nicht leer sein.");
        
        if (string.IsNullOrWhiteSpace(dateProduction) || string.IsNullOrWhiteSpace(datePermit))
            throw new WrongInputException("Zulassungsdatum und Produktionsdatum dürfen nicht leer sein.");
        
        if (!int.TryParse(quantity, out var quantityValue) || quantityValue < 0)
            throw new WrongInputException("Bestand muss eine positive Ganzzahl sein.");
        
        if (!decimal.TryParse(price, out var priceValue) || priceValue < 0)
            throw new WrongInputException("Preis muss eine positive Zahl sein.");

        if (!int.TryParse(ps, out var psValue) || psValue < 0)
            throw new WrongInputException("PS muss eine positive Ganzzahl sein.");

        Car c = new Car();
        c.Name = name;
        c.DateProduction = dateProduction;
        c.DatePermit = datePermit;
        c.Brand = brand;
        c.Model = model;
        c.PS = psValue;
        c.Quantity = quantityValue;
        c.Price = priceValue;
        await _carInventory.InsertCarAsync(c);
        var carDto = new CarDto
        {
            Brand = c.Brand,
            DatePermit = c.DatePermit,
            DateProduction = c.DateProduction,
            Price = c.Price,
            PS = c.PS,
            Model = c.Model,
            Name = c.Name,
            Quantity = c.Quantity,
            Id = c.Id,
        };
        return carDto;
    }

    public async Task<CarDto> GetCarByIdAsync(int id)
    {
        var car = await _carInventory.GetCarByIdAsync(id);
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

    public async Task DeleteCarAsync(CarDto car)
    {
        var productsCount = (await _productInventory.GetProductsForCarsAsync(car.Id)).Count;
        if (productsCount > 0){
            throw new ForbiddenActionException("Fahrzeug besitzt "  + productsCount + " registrierte Produkte und darf nicht gelöscht werden.");
        }
        var carToDelete = await _carInventory.GetCarByIdAsync(car.Id);
        await _carInventory.DeleteCarAsync(carToDelete);
        _logger.LogInformation("Car deleted");
    }

    public async Task UpdateCarAsync(CarDto car)
    {
        var carToUpdate = await _carInventory.GetCarByIdAsync(car.Id);
        carToUpdate.Brand = car.Brand;
        carToUpdate.DatePermit = car.DatePermit;
        carToUpdate.DateProduction = car.DateProduction;
        carToUpdate.Price = car.Price;
        carToUpdate.Model = car.Model;
        carToUpdate.Quantity = car.Quantity;
        carToUpdate.PS = car.PS;
        carToUpdate.Name = car.Name;
        
        await _carInventory.UpdateCarAsync(carToUpdate);
        _logger.LogInformation(
            "Car updated. CarId={CarId}, Name={Name}",
            car.Id,
            car.Name
        );
    }

    public async Task<List<ProductDto>> GetAvailableRims(int carId)
    {
        var query = new ProductListQuery
        {
            CarId = carId,
            Type = ProductType.Felge,
        };
        var rims = await _productInventory.QueryAsync(query);
        return rims.ConvertAll(p => new ProductDto
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
    

    public async Task<List<ProductDto>> GetAvailableColors(int carId)
    {
        var query = new ProductListQuery
        {
            CarId = carId,
            Type = ProductType.Lack,
        };
        var colors = await _productInventory.QueryAsync(query);
        return colors.ConvertAll(p => new ProductDto
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

    public async Task<List<CarDto>> CarListQueryAsync(CarListQuery q)
    {
        
      var cars = await _carInventory.QueryAsync(q);
      return cars.ConvertAll(p => new CarDto
      {
          Id = p.Id,
          Brand = p.Brand,
          DatePermit = p.DatePermit,
          DateProduction = p.DateProduction,
          Price = p.Price,
          PS = p.PS,
          Model = p.Model,
          Name = p.Name,
          Quantity = p.Quantity,
      });
    }
}