using Microsoft.EntityFrameworkCore;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Persistence;

namespace PimpYourBlech_ClassLibrary.Inventories.Implementation;

public sealed class CarInventory(IDatabase database) : ICarInventory
{

    public async Task InsertCarAsync(Car c)
    {

        database.Cars.Add(c);
        await database.SaveChangesAsync();

    }

    public List<Car> ListCars()
    {
        return database.Cars.ToList();

        //  return _cars;
    }

    public List<Product> GetAvailableColor(int Id)
    {
        return database.Products
            .Where(p => p.CarId == Id && p.ColorDetail != null)
            .Include(p => p.ColorDetail)
            .ToList();

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
}