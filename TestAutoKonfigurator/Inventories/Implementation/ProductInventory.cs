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
      database.CreateProduct(p);
    }

    public List<Product> ListProducts()
    {
        return database.LoadProducts();
    }
    
    // _____________________________________ nur für die konsole erstmal __________________________________________

    
    public List<Product> ListEngines()
    {
        var engines = new List<Product>();
        foreach (Product product in database.LoadProducts())
        {
            if (product is Engine)
            {
                engines.Add(product);
            }
        }
        return engines;
    }

    public void InsertEngine(string name,
        string articleNumber,
        string brand,
        string description,
        int quantity,
        double price,
        int _ps,
        int _kw,
        string _displacement,
        Gear _gear)
    {
        Engine engine = new Engine();
        engine.Name = name;
        engine.ArticleNumber = articleNumber;
        engine.Brand = brand;
        engine.Description = description;
        engine.Quantity = quantity;
        engine.Price = price; 
        engine.Ps = _ps;
        engine.Kw = _kw;
        engine.Displacement = _displacement;
        database.CreateProduct(engine);
    }
}