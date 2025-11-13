using TestAutoKonfigurator.Exceptions;
using TestAutoKonfigurator.Interfaces;

namespace TestAutoKonfigurator.Admin;

public sealed class CustomerInventory(IJsonDatabase database):ICustomerInventory
{
    private readonly List<Customer> _customers = database.LoadCustomers(); // Laden der Daten
    private readonly List<String> _customerUsernames = database.LoadCustomerUsernames();

    public void InsertCustomer(Customer c)
    {
        
        
        _customers.Add(c);
        database.SaveCustomers(_customers);
        database.SaveCustomerUsernames(_customers);
    }
    public List<Customer> ListCustomers()
    {
        
        return _customers;
    }

    public List<String> ListCustomerUsernames()
    {
        return _customerUsernames;
    }

    public void DeleteCustomers()
    {
        _customers.Clear();
        database.SaveCustomers(_customers);
        database.SaveCustomerUsernames(_customers);
    }

    public void DeleteCustomer(String username)
    {
        throw new NotImplementedException();
    }

    public void DeleteCustomer(Customer c)
    {
        _customers.Remove(c);
        database.SaveCustomers(_customers);
        database.SaveCustomerUsernames(_customers);
    }

    public void UpdateCustomer(Customer c,String username, String passwordHash, String telefon)
    {
        c.Username = username;
        c.PasswordHash = passwordHash;
        c.Telefon = telefon;
        database.SaveCustomers(_customers);
        database.SaveCustomerUsernames(_customers);
        
    }
    public void UpdateCustomers()
    {
        database.SaveCustomers(_customers);
        database.SaveCustomerUsernames(_customers);
    }

    public Customer GetCustomerByUsername(String username)
    {
        Customer temp = null;
        foreach (Customer c in _customers)
        {
            if (c.Username == username)
                {
                temp = c;
                }
        }

        if (temp == null)
        {
            throw new NoCustomerFoundException("Kein Benutzer mit diesem Username gefunden.");
        }
        return temp;
    }

   

    public Customer GetCustomerByTelefon(String telefon)
    {
        Customer temp = null;
        foreach (Customer c in _customers)
        {
            if (c.Telefon == telefon)
            {
                temp = c;
            }
        }
        
        if (temp == null)
        {
            throw new NoCustomerFoundException("Kein Benutzer mit dieser Telefonnummer gefunden.");
        }

        return temp;
    }

    public Customer GetCustomerAccount(String username, String passwordHash)
    {
        Customer temp = null;
        foreach (Customer c in _customers)
        {
            if (c.Username == username && c.PasswordHash == passwordHash)
            {
                temp = c;
            }

        }
        return temp;
    }
    
    
    public bool LoginBlockedChecker(string username, string hash)
    {
        bool locked = true;
        foreach (Customer c in _customers)
        {
            if (c.Username == username && c.PasswordHash == hash)
            {
                locked = false;
                break;
            }

        }

        return locked;
    }
    
    public Customer GetCustomerByNames(String firstName, String lastName)
    {
        Customer temp = null;
        foreach (Customer c in _customers)
        {
            if (c.FirstName== firstName&&c.LastName==lastName)
            {
                temp = c;
            }
            
            if (temp == null)
            {
                throw new NoCustomerFoundException("Kein Benutzer mit diesen Namen gefunden.");
            }
        }

        return temp;
    }
       
    public IList<Customer> Search(string searchTerm)
    {
        var alle = ListCustomers();
        IList<Customer> result = new List<Customer>();

        foreach (var b in alle)
        {
            // IndexOf returns -1 when substring was not found
            if (b.Username.Contains(searchTerm, StringComparison.CurrentCulture))
            {
                result.Add(b);
            }
        }
        return result;
    }
    
}