using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.Services.Customers;

public interface ICustomerService
{
    public Task<List<DeliveryAddressDto>> GetUserAddressesAsync(int customerId);
    

}