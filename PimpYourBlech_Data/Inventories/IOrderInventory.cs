using PimpYourBlech_Contracts.DTOs;
using PimpYourBlech_Data.Models;

namespace PimpYourBlech_Data.Inventories;

public interface IOrderInventory
{
    public Task<List<OrderPosition>> GetProductsForOrderAsync(int orderId);
    
    public Task<List<Order>> GetOrdersIncludeCustomerAsync();
    public Task<List<Order>> GetOrdersForCustomerIncludeCustomerAsync(int customerId);
    
    public Task<List<Order>> GetOrdersForCustomerAsync(int customerId);
    
    public Task<List<Order>> GetAllOrdersAsync();
    
    public Task<DeliveryAddress> GetDeliveryAddressForOrderAsync(int orderId);
    
}