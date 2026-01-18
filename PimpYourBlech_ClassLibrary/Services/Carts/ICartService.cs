using PimpYourBlech_ClassLibrary.ValueObjects;
using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.Services.Carts;

public interface ICartService
{
    // Fügt ein Produkt dem Warenkorb hinzu
    Task AddProduct(Cart cart, ProductDto product);

    // Entfernt ein Produkt aus dem Warenkorb
    void RemoveProduct(Cart cart, CartPosition cp);

    // Berechnet den Gesamtpreis des Warenkorbs
    public decimal GetTotalPrice(Cart cart);
}