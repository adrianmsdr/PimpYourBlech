using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PimpYourBlech_Contracts.Enums;
using PimpYourBlech_Contracts.Query;
using PimpYourBlech_Data.Models;
using PimpYourBlech_Data.Persistence;

namespace PimpYourBlech_Data.Inventories.Implementation;

public sealed class CarInventory(IDatabase database) : ICarInventory
{

    public async Task InsertCarAsync(Car c)
    {

        database.Cars.Add(c);
        await database.SaveChangesAsync();

    }

    public async Task<List<Car>> ListCarsAsync()
    {
        return await database.Cars.ToListAsync();

        //  return _cars;
    }

    public async Task<List<Car>> ListCarsForConfigurationsAsync()
    {
        return await database.Cars.Where(c =>
                database.Products.Any(p => p.CarId == c.Id && p.ProductType == ProductType.Lack) &&
                database.Products.Any(p => p.CarId == c.Id && p.ProductType == ProductType.Motor) &&
                database.Products.Any(p => p.CarId == c.Id && p.ProductType == ProductType.Felge))
            .ToListAsync();
    }

    public async Task<List<Product>> GetAvailableColorsAsync(int Id)
    {
        return await database.Products
            .Where(p => p.CarId == Id && p.ColorDetail != null)
            .Include(p => p.ColorDetail)
            .ToListAsync();

    }

    public async Task<List<Product>> GetAvailableLightsAsync(int Id)
    {
        return await database.Products
            .Where(p => p.CarId == Id && p.LightsDetail != null)
            .Include(p => p.LightsDetail)
            .ToListAsync();
    }

    public async Task<List<Product>> GetAvailableProductsAsync(int Id, ProductType type)
    {

        IQueryable<Product> query = database.Products.AsNoTracking().Where(p => p.CarId == Id);


        if (type == ProductType.Lack)
            query = query.Where(p => p.ProductType == ProductType.Lack);

        if (type == ProductType.Motor)
            query = query.Where(p => p.ProductType == ProductType.Motor);

        if (type == ProductType.Felge)
            query = query.Where(p => p.ProductType == ProductType.Felge);


        return await query.ToListAsync();

    }

    public async Task DeleteCarAsync(Car car)
    {
        database.Cars.Remove(car);
        await database.SaveChangesAsync();
    }

    public async Task UpdateCarAsync(Car car)
    {
        database.Cars.Update(car);
        await database.SaveChangesAsync();
    }

    public async Task<Car?> GetCarByIdAsync(int Id)
    {
        return await database.Cars.FindAsync(Id);
    }

    public async Task<List<Product>> GetAvailableEnginesAsync(int carId)
    {
        return await database.Products
            .Include(p => p.EngineDetail)
            .Where(p => p.CarId == carId
                                                  && p.ProductType == ProductType.Motor).ToListAsync();
    }

    public async Task<List<Product>> GetAvailableRimsAsync(int carId)
    {
        return await database.Products.Where(p => p.CarId == carId
                                                  && p.ProductType == ProductType.Felge).ToListAsync();
    }

    public async Task<List<Product>> GetAvailableExtrasAsync(int carId)
    {
       
            return await database.Products.Where(p => p.CarId == carId)
                .Where(p=>p.ProductType!=ProductType.Lack&&p.ProductType!=ProductType.Motor&&p.ProductType!=ProductType.Felge&&p.ProductType!=ProductType.Lichter).ToListAsync();
        
    }

    public async Task<List<Car>> QueryAsync(CarListQuery q)
    {
        IQueryable<Car> query = database.Cars.AsNoTracking();

        if (q.CarId is not null)
            query = query.Where(c => c.Id == q.CarId.Value);

        if (!string.IsNullOrWhiteSpace(q.NameContains))
            query = query.Where(c => c.Name.Contains(q.NameContains));

        if (!string.IsNullOrWhiteSpace(q.Brand))
            query = query.Where(c => c.Brand == q.Brand);

        if (q.MinPrice is not null)
            query = query.Where(p => p.Price >= q.MinPrice.Value);

        if (q.MaxPrice is not null)
            query = query.Where(p => p.Price <= q.MaxPrice.Value);

        query = q.SortBy switch
        {
            CarSort.PriceAsc => query.OrderBy(p => p.Price),
            CarSort.PriceDesc => query.OrderByDescending(p => p.Price),
            CarSort.NameDesc => query.OrderByDescending(p => p.Name),
            CarSort.NameAsc => query.OrderBy(p => p.Name),
        };

        return await query.ToListAsync();

    }
}