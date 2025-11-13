namespace TestAutoKonfigurator.Interfaces;

public interface IProductInventory
{
    public void InsertProduct(Product product);
    
    public List<Product> ListProducts();
}