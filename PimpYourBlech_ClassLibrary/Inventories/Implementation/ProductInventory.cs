using Microsoft.EntityFrameworkCore;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Persistence;

namespace PimpYourBlech_ClassLibrary.Inventories.Implementation;

public sealed class ProductInventory(IDatabase database):IProductInventory
{
    
    
    public async Task InsertProduct(Product p)
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
    
    public void DeleteProduct(Product p)
    {
       database.Products.Remove(p);
       database.SaveChanges();
    }

    public void UpdateProduct(Product p)
    {
        database.Products.Update(p);
        database.SaveChanges();
    }
    
    public List<Product> ListEngines()
    {
        return database.Products
            .Where(p => p.EngineDetail != null)
            .Include(p => p.EngineDetail)
            .ToList();
    }

    public List<Product> ListRims()
    {
        return database.Products
            .Where(p => p.RimDetail != null)
            .ToList();
    }


    
}