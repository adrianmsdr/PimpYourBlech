using TestAutoKonfigurator.Exceptions;
using TestAutoKonfigurator.Persistence;

namespace TestAutoKonfigurator.Inventories.Implementation;

public sealed class CustomerInventory(IDatabase database):ICustomerInventory
{
    private readonly List<Customer> _customers = database.LoadCustomers(); // Laden der Daten

    public void InsertCustomer(Customer c)
    {
        
        
        _customers.Add(c);
        database.SaveCustomers(_customers);
    }
    public List<Customer> ListCustomers()
    {
        
        return _customers;
    }

    

    public void DeleteCustomers()
    {
        _customers.Clear();
        database.SaveCustomers(_customers);
    }
    

    public void DeleteCustomer(Customer c)
    {
        _customers.Remove(c);
        database.SaveCustomers(_customers);
    }

    public void UpdateCustomer(Customer c,String username, String passwordHash, String telefon)
    {
        c.Username = username;
        c.PasswordHash = passwordHash;
        c.Telefon = telefon;
        database.SaveCustomers(_customers);

        
    }
    public void UpdateCustomers()
    {
        database.SaveCustomers(_customers);

    }

    

   


    
    
    
}