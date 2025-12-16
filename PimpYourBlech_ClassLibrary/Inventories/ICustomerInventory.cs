using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Inventories;

public interface ICustomerInventory
{
    public void InsertCustomer(Customer customer);
    
    public List<Customer> ListCustomers();
    
  
    public void DeleteCustomers();
    
   
    
    public void UpdateCustomer(Customer customer,string username,string passwordHash,string telefon);
    
    public void UpdateCustomers();
    
    public void DeleteCustomer(Customer customer);

    public void AddConfiguration(Configuration config);

    public List<Configuration> GetConfigurationsForCustomer(int customerId);

    public Task AddCommunityQuestionAsync(string content);

    public Task AddCommunityAnswerAsync(int questionId, string content);

    public Task<List<CommunityQuestion>> GetCommunityQuestionsAsync();
    public List<Configuration> ListConfigurations();
}