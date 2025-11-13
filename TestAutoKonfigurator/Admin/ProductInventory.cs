using TestAutoKonfigurator.Interfaces;

namespace TestAutoKonfigurator.Admin;

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
    
    
}