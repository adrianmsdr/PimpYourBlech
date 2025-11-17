namespace TestAutoKonfigurator.Inventories.InventoryService;

public interface ICustomerService
{
    
    
    public List<Customer> GetListCustomers();

    Customer Register(string firstName, string lastName, string username, string passwordHash, string telefon,
        string mailAddress);
    Customer Login(string username, string passwordHash);
    bool isUsernameAvailable(string username);
    
    bool LoginChecker(string username, string passwordHash);
    
    Customer GetCustomer(string username, string passwordHash);
    
    void DeleteAllCustomers();
    
    void DeleteCustomer(Customer c);
    
    public Customer GetCustomerByUsername(String username);
    
    public Customer GetCustomerByTelefon(String telefone);
    
    public Customer GetCustomerByNames(String firstName, String lastName);

    public void UpdateCustomer(Customer customer, String username, String passwordHash, String telefon);

    void UpdateCustomers();
    
}