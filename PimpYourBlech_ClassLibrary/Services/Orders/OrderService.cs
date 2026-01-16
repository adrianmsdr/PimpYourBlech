using Microsoft.Extensions.Logging;
using PimpYourBlech_ClassLibrary.Session;
using PimpYourBlech_ClassLibrary.ValueObjects;
using PimpYourBlech_Contracts.DTOs;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Enums;
using PimpYourBlech_Data.Inventories;
using PimpYourBlech_Data.Models;

namespace PimpYourBlech_ClassLibrary.Services.Orders;

public class OrderService : IOrderService
{
    private readonly ICustomerInventory _customerInventory;
    private readonly IOrderInventory _orderInventory;
    private readonly ILogger<OrderService> _logger;

    public OrderService(ICustomerInventory customerInventory, ILogger<OrderService> logger,
        IOrderInventory orderInventory)
    {
        _customerInventory = customerInventory;
        _logger = logger;
        _orderInventory = orderInventory;
    }

    public async Task<OrderDto> CreateOrderFromCart(Cart cart, CustomerDto customer, DeliveryAddressDto address,
        PaymentValueDto pv)
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

        _customerInventory.AddOrder(order);

        _logger.LogInformation(
            "Order successfully created for CustomerId={CustomerId}",
            customer.Id
        );
        var orderDto = await _customerInventory.GetOrderByIdAsync(order.OrderId);

        return orderDto != null
            ? new OrderDto
            {
                OrderId = orderDto.OrderId,
                CustomerId = orderDto.CustomerId,
                OrderDate = orderDto.OrderDate,
                TotalPrice = orderDto.TotalPrice,
                DeliveryAddressId = orderDto.DeliveryAddressId,
                PaymentValueId = orderDto.PaymentValueId,
            }
            : null;
    }

    public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
    {
        _logger.LogInformation("Fetching customer with addresses (CustomerId={CustomerId})", id);
        var customer = await _customerInventory.GetCustomerByIdAsync(id);

        return customer != null
            ? new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Username = customer.Username,
                Telefon = customer.Telefon,
                MailAddress = customer.MailAddress,
                AdminRights = customer.AdminRights,
                PasswordHash = customer.PasswordHash,
                ImagePath = customer.ImagePath,
            }
            : null;
    }

    public async Task<DeliveryAddressDto?> GetDeliveryAddressAsync(int id)
    {
        _logger.LogInformation("Fetching delivery address (AddressId={AddressId})", id);
        var deliveryAddress = await _customerInventory.GetDeliveryAddressAsync(id);
        return deliveryAddress != null
            ? new DeliveryAddressDto
            {
                Id = deliveryAddress.Id,
                Salutation = deliveryAddress.Salutation,
                Surname = deliveryAddress.Surname,
                Lastname = deliveryAddress.Lastname,
                Street = deliveryAddress.Street,
                HouseNumber = deliveryAddress.HouseNumber,
                Town = deliveryAddress.Town,
                PostalCode = deliveryAddress.PostalCode,
                Country = deliveryAddress.Country,
            }
            : null;
    }

    public async Task<int> InsertDeliveryAddressAsync(DeliveryAddressDto address)
    {
        var deliveryAddress = new DeliveryAddress()
        {
            Id = address.Id,
            Salutation = address.Salutation,
            Surname = address.Surname,
            Lastname = address.Lastname,
            Street = address.Street,
            HouseNumber = address.HouseNumber,
            Town = address.Town,
            PostalCode = address.PostalCode,
            Country = address.Country,
        };


        _logger.LogInformation(
            "Inserted delivery address (AddressId={AddressId}", address.Id
        );
        return await _customerInventory.InsertDeliveryAddressAsync(deliveryAddress);
    }

    public async Task<int> InsertPaymentValueAsync(PaymentValueDto paymentValue)
    {
        var payment = new PaymentValue()
        {
            Id = paymentValue.Id,
            AccountOwner = paymentValue.AccountOwner,
            CustomerId = paymentValue.CustomerId,
            Iban = paymentValue.Iban,
            Bic = paymentValue.Bic,
        };

        return await _customerInventory.InsertPaymentValueAsync(payment);
    }

    public async Task<PaymentValueDto?> GetPaymentValueAsync(int id)
    {
        var paymentValue = await _customerInventory.GetPaymentValueAsync(id);
        return paymentValue != null
            ? new PaymentValueDto
            {
                Id = paymentValue.Id,
                AccountOwner = paymentValue.AccountOwner,
                CustomerId = paymentValue.CustomerId,
                Iban = paymentValue.Iban,
                Bic = paymentValue.Bic,
            }
            : null;
    }

    public async Task<List<PaymentValueDto>> GetPaymentValuesAsync(int id)
    {
        var paymentValues = await _customerInventory.GetPaymentValuesAsync(id);
        return paymentValues.ConvertAll(p => new PaymentValueDto()
        {
            Id = p.Id,
            AccountOwner = p.AccountOwner,
            CustomerId = p.CustomerId,
            Iban = p.Iban,
            Bic = p.Bic,
        });
    }


    public async Task<List<OrderCustomerDto>> GetOrdersBasicsAsync()
    {
        var orders = await _orderInventory.GetOrdersIncludeCustomerAsync();
        return orders.ConvertAll(o => new OrderCustomerDto
        {
            Created = o.OrderDate,
            CustomerFirstName = o.Customer.FirstName,
            CustomerLastName = o.Customer.LastName,
            Id = o.OrderId,
            TotalPrice = o.TotalPrice,
        });
    }

    public async Task<List<OrderPositionDto>> GetOrdersPositionsAsync(int orderId)
    {
        var orderPositions = await _orderInventory.GetProductsForOrderAsync(orderId);
        return orderPositions.ConvertAll(o => new OrderPositionDto
        {
            OrderPositionId = o.OrderPositionId,
            OrderId = o.OrderId,
            ArticleNumber = o.ArticleNumber,
            Name = o.Name,
            Brand = o.Brand,
            Type = o.Type,
            Quantity = o.Quantity,
            UnitPrice = o.UnitPrice,
        });
    }
    
    public async Task<List<OrderDto>> GetUserOrdersAsync(int customerId)
    {
        var orders = await _orderInventory.GetOrdersForCustomerAsync(customerId);
        return orders.ConvertAll(o => new OrderDto
        {
            OrderId = o.OrderId,
            CustomerId = o.CustomerId,
            OrderDate = o.OrderDate,
            TotalPrice = o.TotalPrice,
            DeliveryAddressId = o.DeliveryAddressId,
            PaymentValueId = o.PaymentValueId,
        });
    }

    public async  Task<DeliveryAddressDto?> GetDeliveryAddressForOrderAsync(int orderId)
    {
        var da = await _orderInventory.GetDeliveryAddressForOrderAsync(orderId);
        return da != null
            ? new DeliveryAddressDto()
            {
                Id = da.Id,
                Salutation = da.Salutation,
                Surname = da.Surname,
                Lastname = da.Lastname,
                Street = da.Street,
                HouseNumber = da.HouseNumber,
                Town = da.Town,
                PostalCode = da.PostalCode,
                Country = da.Country,
            }
            : null;
    }
}