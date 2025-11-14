namespace TestAutoKonfigurator.Interfaces.Inventories;

public interface IProductInventory
{
    public void InsertProduct(Product product);
    
    public List<Product> ListProducts();
}