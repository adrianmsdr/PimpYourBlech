using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Services.Shop;

public interface IShopService
{
    // Produktübersicht 
    //  IList<Product> GetAllProducts();
    //Product GetProductById(int productId);

    // Warenkorb-Operationen
    //  void AddToCart(Customer customer, int productId, int quantity = 1);
    //void RemoveFromCart(int userId, int productId);
    // void ClearCart(int userId);

    // Warenkorb anzeigen 
    // Cart GetCart(int userId);

    //  Preise 
    double GetCartTotal(int userId);

    //(Optional für später) Bestellen
    public List<Product> GetProducts();
    
    public List<Product> SearchProducts(string searchString);

    public List<Product> FilterProducts(string selectedCategory);
}