using Microsoft.Extensions.Logging;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Inventories;
using PimpYourBlech_ClassLibrary.Session;
using PimpYourBlech_ClassLibrary.ValueObjects;

namespace PimpYourBlech_ClassLibrary.Services.Orders;

public class OrderService : IOrderService
{
    private readonly ICustomerInventory _customerInventory;
    private readonly ILogger<OrderService> _logger;
    public OrderService(ICustomerInventory customerInventory, ILogger<OrderService> logger)
    {
        _customerInventory = customerInventory;
        _logger = logger;
    }
    
    public async Task<Order> CreateOrderFromCart(Cart cart, Customer customer, DeliveryAddress address, PaymentValue pv)
    {
        _logger.LogInformation(
            "Creating order from cart for CustomerId={CustomerId}",
            customer.Id
        );
        
        await Task.Delay(1);
        if (cart.Products.Count == 0)
        {
            _logger.LogWarning(
                "Order creation failed: Cart is empty (CustomerId={CustomerId})",
                customer.Id
            );
            
            throw new InvalidOperationException("Cart is empty");
        }

        var order = new Order
        {
            CustomerId = customer.Id,
            OrderDate = DateTime.UtcNow,
            Items = new List<OrderPosition>(),
            TotalPrice = cart.Products.Sum(p => p.Price),
            DeliveryAddressId = address.Id,
            PaymentValueId = pv.Id,
        };

        foreach (var p in cart.Products)
        {
            order.Items.Add(new OrderPosition
            {
                Name = p.Product.Name,
                ArticleNumber = p.Product.ArticleNumber,
                Brand = p.Product.Brand,
                Type = p.Product.ProductType,
                Quantity = p.Quantity,
                UnitPrice = (decimal)p.Price,
            });
        }
        
        _logger.LogInformation(
            "Order contains {ItemCount} items with total price {TotalPrice}",
            order.Items.Count,
            order.TotalPrice
        );
        /*

        var existingAddress = customer.DeliveryAddresses;

        if (existingAddress.Contains(address))
        {
            order.DeliveryAddressId = address.Id;

            _logger.LogInformation(
                "Using existing delivery address (AddressId={AddressId})",
                address.Id
            );
        }
        else
        {
            address.CustomerId = customer.Id;
            address.Customer = null;
            order.DeliveryAddress = address;

            _logger.LogInformation(
                "Using new delivery address for CustomerId={CustomerId}",
                customer.Id
            );
             }
            */        

        _customerInventory.AddOrder(order);
        
        _logger.LogInformation(
            "Order successfully created for CustomerId={CustomerId}",
            customer.Id
        );
        
        return order;
    }

    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        _logger.LogInformation("Fetching customer with addresses (CustomerId={CustomerId})", id);
        return await _customerInventory.GetCustomerIncludeAdressesAsync(id);
    }

    public async Task<DeliveryAddress> GetDeliveryAddressAsync(int id)
    {
        _logger.LogInformation("Fetching delivery address (AddressId={AddressId})", id);
        return await _customerInventory.GetDeliveryAddressAsync(id);
    }
    
    public async Task<int> InsertDeliveryAddressAsync(DeliveryAddress address)
    {
        _logger.LogInformation(
            "Inserted delivery address (AddressId={AddressId}, CustomerId={CustomerId})",
            address.CustomerId
        );
        return await _customerInventory.InsertDeliveryAddressAsync(address);
    }

    public async Task<int> InsertPaymentValueAsync(PaymentValue paymentValue)
    {
        return await _customerInventory.InsertPaymentValueAsync(paymentValue);
    }

    public async Task<PaymentValue?> GetPaymentValueAsync(int id)
    {
        return await _customerInventory.GetPaymentValueAsync(id);
    }

    public async Task<List<PaymentValue>> GetPaymentValuesAsync(int id)
    {
        return await _customerInventory.GetPaymentValuesAsync(id);
    }
}