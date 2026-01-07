using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Services.Shop;

public interface IShopService
{
    

    //(Optional für später) Bestellen
    public List<Product> GetProducts();
    
    public List<Product> SearchProducts(string searchString);

    public List<Product> FilterProducts(string selectedCategory);
}