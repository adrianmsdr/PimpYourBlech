using PimpYourBlech_ClassLibrary.Exceptions;
using PimpYourBlech_ClassLibrary.Services.Orders;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Data.Inventories;

namespace PimpYourBlech_ClassLibrary.Services.Customers.Implementation;

public class CustomerService:ICustomerService
{
    private readonly ICustomerInventory _customerInventory;
    private readonly IOrderInventory _orderInventory;

    public CustomerService(ICustomerInventory customerInventory, IOrderInventory orderInventory)
    {
        _customerInventory = customerInventory;
        _orderInventory = orderInventory;
    }


    public async Task<List<DeliveryAddressDto>> GetUserAddressesAsync(int customerId)
    {
        var adresses = await _customerInventory.GetUserAddressesAsync(customerId);
        return adresses.ConvertAll(d => new DeliveryAddressDto
        {
            Id = d.Id,
            Country = d.Country,
            CustomerId = d.CustomerId,
            HouseNumber = d.HouseNumber,
            Lastname = d.Lastname,
            PostalCode = d.PostalCode,
            Salutation = d.Salutation,
            Street = d.Street,
            Surname = d.Surname,
            Town = d.Town,
        });
    }

    public async Task CanBeDeletedAsync(int customerId)
    {
        if ((await _orderInventory.GetOrdersForCustomerAsync(customerId)).Any())
        {
            throw new ForbiddenActionException(
                "User hat registrierte Bestellungen und darf daher nicht gelöscht werden.");
        }

        if ((await _customerInventory.GetUserAddressesAsync(customerId)).Any())
        {
            throw new ForbiddenActionException(
                "User hat registrierte Adressen und darf daher nicht gelöscht werden.");
        }
        
        if ((await _customerInventory.GetPaymentValuesAsync(customerId)).Any())
        {
            throw new ForbiddenActionException(
                "User hat registrierte Zahlungsmethoden und darf daher nicht gelöscht werden.");
        }
    }
}