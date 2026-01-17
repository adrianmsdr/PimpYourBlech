using Microsoft.EntityFrameworkCore;
using PimpYourBlech_Data.Models;
using PimpYourBlech_Data.Persistence;

namespace PimpYourBlech_Data.Inventories.Implementation;

public class OrderInventory(IDatabase database):IOrderInventory
{

    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        return await database.Orders
            .FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    public async Task<DeliveryAddress?> GetDeliveryAddressByIdAsync(int deliveryAddressId)
    {
        return await database.DeliveryAddresses
            .FirstOrDefaultAsync(a => a.Id == deliveryAddressId);
    }
    public async Task<List<OrderPosition>> GetProductsForOrderAsync(int orderId)
    {
        var order = await database.Orders
            .Include(o => o.Items)
            .Where(o => o.OrderId == orderId).FirstOrDefaultAsync();
        
        return order.Items;
    }

    public async Task<List<Order>> GetOrdersIncludeCustomerAsync()
    {
        return await database.Orders
            .Include(o => o.Customer)
            .ToListAsync();
    }

    public async Task<List<Order>> GetOrdersForCustomerAsync(int customerId)
    {
        return await database.Orders
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await database.Orders.ToListAsync();
    }

    public async Task<DeliveryAddress> GetDeliveryAddressForOrderAsync(int orderId)
    {
       var order = await GetOrderByIdAsync(orderId);
      var address = await GetDeliveryAddressByIdAsync(order.DeliveryAddressId);
      return address;
       

    }
}