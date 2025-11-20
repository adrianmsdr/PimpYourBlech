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
}