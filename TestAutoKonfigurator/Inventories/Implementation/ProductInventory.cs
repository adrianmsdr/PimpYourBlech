using TestAutoKonfigurator.Enums;
using TestAutoKonfigurator.Persistence;

namespace TestAutoKonfigurator.Inventories.Implementation;

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
    
    // _____________________________________ nur für die konsole erstmal __________________________________________

    
    public List<Product> ListEngines()
    {
        var engines = new List<Product>();
        foreach (Product product in database.Products)
        {
            if (product is Engine)
            {
                engines.Add(product);
            }
        }
        return engines;
    }

    public void InsertEngine(Engine engine)
    {
        
        database.Products.Add(engine);
       database.SaveChanges();
    }
}