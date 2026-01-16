using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Query;
using PimpYourBlech_Data.Models;

namespace PimpYourBlech_Data.Inventories;

public interface ICustomerInventory
{
    public Task InsertCustomerAsync(Customer customer);
    
    public List<Customer> ListCustomers();
    
    public Task<List<Customer>> ListCustomersAsync();
    
    Task<Customer> GetCustomerByIdIncludeAllAsync(int id);

    Task<Customer?> GetCustomerByIdAsync(int id);

    Task<Customer?> GetCustomerIncludeAdressesAsync(int id);
    
    public Task UpdateCustomerAsync(Customer customer);
    
    public void UpdateCustomers();
    
    public Task DeleteCustomerAsync(Customer customer);

    public void AddConfiguration(Configuration config);

    public Task<List<Configuration>> GetConfigurationsForCustomer(int customerId);

    public Task AddCommunityQuestionAsync(string content);

    public Task AddCommunityAnswerAsync(int questionId, string content);

    public Task<List<CommunityQuestion>> GetCommunityQuestionsAsync();
    public List<Configuration> ListConfigurations();
    
    public void DeleteConfiguration(Configuration config);

    void AddOrder(Order order);
    
     
    Task<List<Order>> GetOrdersAsync();
    
    Task<Order> GetOrderByIdAsync(int id);
    
    Task<bool> UsernameExistsAsync(string username);

    public Task<Customer?> GetCustomerByUsernameAsync(string username);

    public Task<DeliveryAddress> GetDeliveryAddressAsync(int id);
    
    Task<int> InsertDeliveryAddressAsync(DeliveryAddress address);
    
    Task<int> InsertPaymentValueAsync(PaymentValue paymentValue);

    public Task<PaymentValue?> GetPaymentValueAsync(int id);
    Task<List<OrderPosition>> GetOrderItemsAsync(int id);
    
    Task<List<PaymentValue>> GetPaymentValuesAsync(int customerId);
    
    Task<List<Customer>> QueryCustomersAsync(CustomerListQuery q);
    
    Task<List<DeliveryAddress>> GetUserAddressesAsync(int customerId);
   
}