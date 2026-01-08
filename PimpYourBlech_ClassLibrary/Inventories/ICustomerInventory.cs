using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Inventories;

public interface ICustomerInventory
{
    public void InsertCustomer(Customer customer);
    
    public List<Customer> ListCustomers();
    
    public Task<List<Customer>> ListCustomersAsync();
    
  
    public void DeleteCustomers();
    
    Task<Customer> GetCustomerByIdAsync(int id);
    
    public Task UpdateCustomerAsync(Customer customer);
    
    public void UpdateCustomers();
    
    public void DeleteCustomer(Customer customer);

    public void AddConfiguration(Configuration config);

    public List<Configuration> GetConfigurationsForCustomer(int customerId);

    public Task AddCommunityQuestionAsync(string content);

    public Task AddCommunityAnswerAsync(int questionId, string content);

    public Task<List<CommunityQuestion>> GetCommunityQuestionsAsync();
    public List<Configuration> ListConfigurations();
    
    public void DeleteConfiguration(Configuration config);

    void AddOrder(Order order);
    
     
    Task<List<Order>> GetOrdersAsync();
    
    Task<Order> GetOrderByIdAsync(int id);
}