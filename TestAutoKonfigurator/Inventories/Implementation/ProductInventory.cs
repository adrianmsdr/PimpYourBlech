using TestAutoKonfigurator.Database;
using TestAutoKonfigurator.Enums;

namespace TestAutoKonfigurator.Inventories.Implementation;

public sealed class ProductInventory(IJsonDatabase database):IProductInventory
{
    
    private readonly List<Product> _products = database.LoadProducts();
    
    public void InsertProduct(Product p)
    {
        _products.Add(p);
        database.SaveProducts(_products);
    }

    public List<Product> ListProducts()
    {
        return _products;
    }
    
    // _____________________________________ nur für die konsole erstmal __________________________________________

    
    public List<Product> ListEngines()
    {
        var engines = new List<Product>();
        foreach (Product product in _products)
        {
            if (product.Type == ProductCategory.Engine)
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
        Product product = new Product();
        product.Name = name;
        product.ArticleNumber = articleNumber;
        product.Brand = brand;
        product.Description = description;
        product.Quantity = quantity;
        product.Price = price; 
        product.Ps = _ps;
        product.Kw = _kw;
        product.Displacement  = _displacement;
        product.Gear = _gear;
        product.Type = ProductCategory.Engine;
        InsertProduct(product);
    }
}