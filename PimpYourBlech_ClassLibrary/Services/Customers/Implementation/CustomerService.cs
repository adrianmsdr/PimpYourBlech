using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Data.Inventories;

namespace PimpYourBlech_ClassLibrary.Services.Customers.Implementation;

public class CustomerService:ICustomerService
{
    private readonly ICustomerInventory _customerInventory;

    public CustomerService(ICustomerInventory customerInventory)
    {
        _customerInventory = customerInventory;
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

   
}