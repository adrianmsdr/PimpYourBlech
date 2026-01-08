using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Inventories;
using PimpYourBlech_ClassLibrary.Session;
using PimpYourBlech_ClassLibrary.ValueObjects;

namespace PimpYourBlech_ClassLibrary.Services.Orders;

public class OrderService : IOrderService
{
    private readonly ICustomerInventory _customerInventory;
    public OrderService(ICustomerInventory customerInventory)
    {
        _customerInventory = customerInventory;
    }
    
    public async Task<Order> CreateOrderFromCart(Cart cart, Customer customer, DeliveryAddress address)
    {
        await Task.Delay(1);
        if (cart.Products.Count == 0)
            throw new InvalidOperationException("Cart is empty");
        
        var order = new Order
        {
            CustomerId = customer.Id,
            Products = cart.Products,
            DeliveryAddress = address,
            OrderDate = DateTime.UtcNow
        };

        _customerInventory.AddOrder(order);
        return order;
    }
}