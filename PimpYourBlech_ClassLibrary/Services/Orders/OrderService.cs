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
            OrderDate = DateTime.UtcNow,
            Items = new List<OrderPosition>(),
            TotalPrice = cart.Products.Sum(p => p.Price)
        };

        foreach (var p in cart.Products)
        {
            order.Items.Add(new OrderPosition
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                UnitPrice = (decimal)p.Price
            });
        }

        var existingAddress = customer.DeliveryAddresses;

        if (existingAddress.Contains(address))
        {
            order.DeliveryAddressId = address.Id;
        }
        else
        {
            address.CustomerId = customer.Id;
            address.Customer = null;
            order.DeliveryAddress = address;
        }

        _customerInventory.AddOrder(order);
        return order;
    }



    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        return await _customerInventory.GetCustomerIncludeAdressesAsync(id);
    }

    public async Task<DeliveryAddress> GetDeliveryAddressAsync(int id)
    {
        return await _customerInventory.GetDeliveryAddressAsync(id);
    }
    
    public async Task InsertDeliveryAddressAsync(DeliveryAddress address)
    {
        await _customerInventory.InsertDeliveryAddressAsync(address);
    }
}