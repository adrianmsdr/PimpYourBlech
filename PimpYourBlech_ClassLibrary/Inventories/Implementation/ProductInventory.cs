using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Persistence;

namespace PimpYourBlech_ClassLibrary.Inventories.Implementation;

public sealed class ProductInventory(IDatabase database):IProductInventory
{
    
    //private readonly List<Product> _products = database.LoadProducts();
    
    public void InsertProduct(Product p)
    {
       // _products.Add(p);
      //  database.SaveProducts(_products);
      //database.CreateProduct(p);
      database.Products.Add(p);
      database.SaveChanges();
    }

    public List<Product> ListProducts()
    {
        return database.Products.ToList();
    }

    public void DeleteProduct(Product p)
    {
       database.Products.Remove(p);
       database.SaveChanges();
    }

    // _____________________________________ nur für die konsole erstmal __________________________________________

    
    public List<Product> ListEngines()
    {
        return database.Products
            .Where(p => p.EngineDetail != null)
            .ToList();
    }

    public List<Product> ListRims()
    {
        return database.Products
            .Where(p => p.RimDetail != null)
            .ToList();
    }


    
}