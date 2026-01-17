using Microsoft.EntityFrameworkCore;

using PimpYourBlech_Contracts.Enums;
using PimpYourBlech_Contracts.Query;
using PimpYourBlech_Data.Models;
using PimpYourBlech_Data.Persistence;

namespace PimpYourBlech_Data.Inventories.Implementation;

public sealed class ProductInventory(IDatabase database):IProductInventory
{
    
    
    public async Task InsertProductAsync(Product p)
    {
        int counter = await database.GetNextArticleNumberAsync();
        p.ArticleNumber = counter.ToString("D7");
        database.Products.Add(p);
         await database.SaveChangesAsync();
    }

    public List<Product> ListProducts()
    {
        return database.Products.ToList();
    }
    
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await database.Products.FindAsync(id);
    }
    
    public async Task DeleteProductAsync(Product p)
    {
       database.Products.Remove(p);
       await database.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product p)
    {
        database.Products.Update(p);
        await database.SaveChangesAsync();
    }
    
    public async Task<List<Product>> ListEnginesAsync()
    {
        return await database.Products
            .Where(p => p.EngineDetail != null)
            .Include(p => p.EngineDetail)
            .ToListAsync();
    }

    public async Task<List<Product>> ListRimsAsync()
    {
        return await database.Products
            .Where(p => p.RimDetail != null)
            .ToListAsync();
    }

    public async Task<List<Product>> QueryAsync(ProductListQuery q)
    {
        IQueryable<Product> query = database.Products.AsNoTracking();

        if (q.ProductId is not null)
            query = query.Where(p => p.ProductId == q.ProductId.Value);

        if (!string.IsNullOrWhiteSpace(q.SearchTerm))
        {
            var term = q.SearchTerm.Trim();
            query = query.Where(p =>
                (p.Name != null && p.Name.Contains(term)) ||
                (p.Brand  != null && p.Brand.Contains(term)) ||
                (p.ArticleNumber  != null && p.ArticleNumber.Contains(term))
            );
        }

        if (!string.IsNullOrWhiteSpace(q.Brand))
            query = query.Where(p => p.Brand == q.Brand);

        if (q.CarId is not null)
            query = query.Where(p => p.CarId == q.CarId.Value);

        if (q.Type is not null)
            query = query.Where(p => p.ProductType == q.Type.Value);

        if (q.MinPrice is not null)
            query = query.Where(p => p.Price >= q.MinPrice.Value);

        if (q.MaxPrice is not null)
            query = query.Where(p => p.Price <= q.MaxPrice.Value);

        query = q.SortBy switch
        {
            ProductSort.PriceAsc  => query.OrderBy(p => p.Price),
            ProductSort.PriceDesc => query.OrderByDescending(p => p.Price),
            ProductSort.NameDesc  => query.OrderByDescending(p => p.Name),
            ProductSort.NameAsc   => query.OrderBy(p => p.Name),
        };

        return await query.ToListAsync();
    }

    public async Task<List<Product>> GetProductsForCarsAsync(int carId)
    {
       return await database.Products
           .Where(p => p.CarId == carId)
           .ToListAsync();
    }
}