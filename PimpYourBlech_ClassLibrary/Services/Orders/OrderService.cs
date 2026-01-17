using System.Numerics;
using System.Text;
using Microsoft.Extensions.Logging;
using PimpYourBlech_ClassLibrary.Exceptions;
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
        ValidateDeliveryAddress(address);
        
        var deliveryAddress = new DeliveryAddress()
        {
            CustomerId = address.CustomerId,
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
        ValidatePaymentValue(paymentValue);
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

    void ValidatePaymentValue(PaymentValueDto paymentValue)
    {
        if (paymentValue == null)
            throw new WrongInputException("Zahlungsdaten fehlen");

        paymentValue.AccountOwner = paymentValue.AccountOwner?.Trim();
        paymentValue.Bic = paymentValue.Bic?.Trim().ToUpperInvariant();
        paymentValue.Iban = paymentValue.Iban?.Replace(" ", "").Trim().ToUpperInvariant();

        if (string.IsNullOrWhiteSpace(paymentValue.AccountOwner))
            throw new WrongInputException("Kontoinhaber fehlt");

        if (string.IsNullOrWhiteSpace(paymentValue.Bic))
            throw new WrongInputException("BIC fehlt");

        if (string.IsNullOrWhiteSpace(paymentValue.Iban))
            throw new WrongInputException("IBAN fehlt");

        if (!IsValidBic(paymentValue.Bic))
            throw new WrongInputException("BIC ist ungültig");

        if (!IsValidIban(paymentValue.Iban))
            throw new WrongInputException("IBAN ist ungültig");
    }
    
    
    void ValidateDeliveryAddress(DeliveryAddressDto address)
    {
        if (address is null) throw new WrongInputException("Adresse fehlt");

        // Trimmen um keine Probleme mit Leerzeichen zu bekommen
        address.Street      = address.Street?.Trim();
        address.HouseNumber = address.HouseNumber?.Trim();
        address.Town        = address.Town?.Trim();
        address.PostalCode  = address.PostalCode?.Trim();
        address.Country     = address.Country?.Trim();
        address.Salutation  = address.Salutation?.Trim();
        address.Surname     = address.Surname?.Trim();
        address.Lastname    = address.Lastname?.Trim();

        if (string.IsNullOrWhiteSpace(address.Street))
            throw new WrongInputException("Straße darf nicht leer sein");

        if (string.IsNullOrWhiteSpace(address.HouseNumber))
            throw new WrongInputException("Hausnummer darf nicht leer sein");

        // Hausnummer: (Zahl + optional Buchstabe (12, 12a, 7B))
        if (!IsValidHouseNumber(address.HouseNumber))
            throw new WrongInputException("Hausnummer ist ungültig (z.B. 12 oder 12a)");

        if (string.IsNullOrWhiteSpace(address.Town))
            throw new WrongInputException("Stadt darf nicht leer sein");

        if (string.IsNullOrWhiteSpace(address.PostalCode))
            throw new WrongInputException("PLZ darf nicht leer sein");

        if (!IsPostalCodeValidForCountry(address.PostalCode, address.Country))
            throw new WrongInputException("PLZ ist für das ausgewählte Land ungültig");

        if (string.IsNullOrWhiteSpace(address.Country))
            throw new WrongInputException("Land darf nicht leer sein");

        if (string.IsNullOrWhiteSpace(address.Lastname)
            || string.IsNullOrWhiteSpace(address.Salutation)
            || string.IsNullOrWhiteSpace(address.Surname))
            throw new WrongInputException("Name und Anrede dürfen nicht leer sein");
        
    }
    
    private static bool IsPostalCodeValidForCountry(string postalCode, string country)
    {
        if (string.IsNullOrWhiteSpace(postalCode) || string.IsNullOrWhiteSpace(country))
            return false;

        // Normalisieren
        var plz = postalCode.Trim();
        

        if (!plz.All(char.IsDigit))
            return false;
        
        return country switch
        {
            "DE"  => plz.Length == 5,
            "AT"  => plz.Length == 4,
            "CH"  => plz.Length == 4,
            _ => false
        };
    }

    private static bool IsValidHouseNumber(string houseNumber)
    { 
        // Erst Ziffern, optional 1 Buchstabe am Ende.
        int i = 0;
        while (i < houseNumber.Length && char.IsDigit(houseNumber[i])) i++;

        if (i == 0) return false;                 // muss mit Zahl anfangen
        if (i == houseNumber.Length) return true; // nur Zahl 

        // genau 1 Buchstabe am Ende, sonst Müll
        return i == houseNumber.Length - 1 && char.IsLetter(houseNumber[i]);
    }
    
    private static bool IsValidIban(string iban)
    {
        if (iban.Length < 15 || iban.Length > 34)
            return false;

        if (!iban.All(char.IsLetterOrDigit))
            return false;

        var countryCode = iban[..2];

        // Längenprüfung pro Land (DE/AT/CH + Reserve)
        var expectedLength = countryCode switch
        {
            "DE" => 22,
            "AT" => 20,
            "CH" => 21,
            _ => -1
        };

        if (expectedLength != -1 && iban.Length != expectedLength)
            return false;

        // Umbauen:
        // iban[..4] = Alles ab Index 4 bis Ende
        // iban[..a] = Index0 bis 3
        var rearranged = iban[4..] + iban[..4];

        var sb = new StringBuilder();

        foreach (char c in rearranged)
        {
            if (char.IsDigit(c))
            {
                sb.Append(c);
            }
            else if (char.IsLetter(c))
            {
                int value = char.ToUpperInvariant(c) - 'A' + 10;
                sb.Append(value);
            }
            else
            {
                throw new WrongInputException("IBAN enthält ungültige Zeichen");
            }
        }

        return Mod97(sb.ToString()) == 1;
    }

    private static int Mod97(string input)
    {
        return (int)(BigInteger.Parse(input) % 97);
    }
    
    private static bool IsValidBic(string bic)
    {
        if (bic.Length != 8 && bic.Length != 11)
            return false;

        // Bankcode (4 Buchstaben)
        if (!bic[..4].All(char.IsLetter))
            return false;
        // Ländercode (2 Buchstaben)
        if (!bic.Substring(4, 2).All(char.IsLetter))
            return false;

        // Ortscode (2 Alphanum)
        if (!bic.Substring(6, 2).All(char.IsLetterOrDigit))
            return false;
        
        return true;
    }
    
    public async Task<List<OrderDto>> GetOrdersAsync()
    {
        var orders = await _customerInventory.GetOrdersAsync();
        return orders.ConvertAll(p => new OrderDto
            {
                CustomerId = p.CustomerId,
                OrderId = p.OrderId,
                OrderDate = p.OrderDate,
                TotalPrice = p.TotalPrice,
                DeliveryAddressId = p.DeliveryAddressId,
                PaymentValueId = p.PaymentValueId,
            }
        );
    }

    public async Task<OrderDto> GetOrderByIdAsync(int id)
    {
        var order = await _customerInventory.GetOrderByIdAsync(id);
        return order != null ? new OrderDto
        {
            CustomerId = order.CustomerId,
            OrderId = order.OrderId,
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPrice,
            DeliveryAddressId = order.DeliveryAddressId,
            PaymentValueId = order.PaymentValueId,
        } : null;
    }

    public async Task<List<OrderPositionDto>> GetOrderItemsAsync(int id)
    {
        var orderitems = await _customerInventory.GetOrderItemsAsync(id);
        return orderitems.ConvertAll(p => new OrderPositionDto
            {
                OrderId = p.OrderId,
                OrderPositionId = p.OrderPositionId,
                ArticleNumber = p.ArticleNumber,
                Name = p.Name,
                Brand = p.Brand,
                Type = p.Type,
                UnitPrice = p.UnitPrice,
                Quantity = p.Quantity,
            }
        );
    }
}