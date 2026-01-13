using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Enums;
using PimpYourBlech_ClassLibrary.Persistence;

namespace PimpYourBlech_ClassLibrary.Inventories.Implementation;

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

    public async Task<List<Product>> GetAvailableProductsAsync(int Id, ProductType type)
    {
        
            IQueryable<Product> query = database.Products.AsNoTracking().Where(p => p.CarId == Id);

            
            if (type == ProductType.Lack)
                query = query.Where(p => p.ProductType== ProductType.Lack);
 
            if (type == ProductType.Motor)
                query = query.Where(p => p.ProductType== ProductType.Motor);
            
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
}