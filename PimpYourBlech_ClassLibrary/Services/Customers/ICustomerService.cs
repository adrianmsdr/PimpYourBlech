using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Query;

namespace PimpYourBlech_ClassLibrary.Services.Customers;

public interface ICustomerService
{
    
    // Getter für Liste aller User
    public Task<List<CustomerDto>> GetListCustomersAsync();
    
    // Neuen User validieren + registrieren 
    public Task RegisterCustomerAsync(string firstName, string lastName, string username, string password,
        string passwordConfirm, string telefon, string mailAddress, string mailAdressConfirm, string ImagePath);

    // Login eines Customers
    Task<CustomerDto> CustomerLoginAsync(string username, string password);



    
    // Getter für die registrieren Lieferadressen eines Users
    public Task<List<DeliveryAddressDto>> GetUserAddressesAsync(int customerId);

    // Checker, ob ein User gelöscht werden darf
    public Task CanBeDeletedAsync(int customerId);
    
    // Löschen eines Users
    Task DeleteCustomerAsync(CustomerDto c);

   
    // Updaten eines Users
    public Task UpdateCustomerAsync(CustomerDto customer);

    // Getter für User über seine ID
    Task<CustomerDto> GetCustomerByIdAsync(int id);
    
    // Query über alle User (Sortierte Liste)
    Task<List<CustomerDto>> GetFilteredCustomersAsync(CustomerListQuery query);
}