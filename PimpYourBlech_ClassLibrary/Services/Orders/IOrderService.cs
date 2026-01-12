using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Session;
using PimpYourBlech_ClassLibrary.ValueObjects;

namespace PimpYourBlech_ClassLibrary.Services.Orders;

public interface IOrderService
{

    Task<Order> CreateOrderFromCart(Cart cart, Customer customer, DeliveryAddress address);
    
    Task<Customer> GetCustomerByIdAsync(int id);

    Task<DeliveryAddress> GetDeliveryAddressAsync(int id);
    
    Task<int> InsertDeliveryAddressAsync(DeliveryAddress address);
}
