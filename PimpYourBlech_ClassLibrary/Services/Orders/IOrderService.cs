using PimpYourBlech_ClassLibrary.ValueObjects;
using PimpYourBlech_Contracts.DTOs;
using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.Services.Orders;

public interface IOrderService
{

    Task<OrderDto> CreateOrderFromCart(Cart cart, CustomerDto customer, DeliveryAddressDto address, PaymentValueDto pv);
    
    Task<CustomerDto?> GetCustomerByIdAsync(int id);

    Task<DeliveryAddressDto?> GetDeliveryAddressAsync(int id);
    
    Task<int> InsertDeliveryAddressAsync(DeliveryAddressDto address);
    
    Task<int> InsertPaymentValueAsync(PaymentValueDto paymentValue);
    
    Task<PaymentValueDto?> GetPaymentValueAsync(int id);

    public Task<List<PaymentValueDto>> GetPaymentValuesAsync(int id);
    
    public Task<List<OrderCustomerDto>> GetOrdersBasicsAsync();
    
    public Task<List<OrderPositionDto>> GetOrdersPositionsAsync(int orderId);
    
   // public Task<List<OrderTotalDto>> GetOrdersTotalAsync();

   public Task<List<OrderDto>> GetUserOrdersAsync(int customerId);
   
   public Task<DeliveryAddressDto?> GetDeliveryAddressForOrderAsync(int orderId);
   
   public Task<List<OrderDto>> GetOrdersAsync();
   
   public Task<OrderDto> GetOrderByIdAsync(int id);

   public Task<List<OrderPositionDto>> GetOrderItemsAsync(int id);
   
}
