namespace TestAutoKonfigurator.Inventories;

public interface ICustomerInventory
{
    public void InsertCustomer(Customer customer);
    
    public List<Customer> ListCustomers();

    public List<String> ListCustomerUsernames();
  
    public void DeleteCustomers();
    
    public void UpdateCustomers();
    
    public void UpdateCustomer(Customer customer,string username,string passwordHash,string telefon);
    
    public void DeleteCustomer(Customer customer);
    
    public Customer GetCustomerByUsername(String username);
    
    public Customer GetCustomerByTelefon(String telefone);
    
    public Customer GetCustomerByNames(String firstName, String lastName);
    
    public bool LoginBlockedChecker(String username, String password);

    public bool UsernameAcceptedChecker(string username);
    
    
    public Customer GetCustomerAccount(String username, String passwordHash);
}