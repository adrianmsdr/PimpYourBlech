using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Persistence;

namespace PimpYourBlech_ClassLibrary.Inventories.Implementation;

public sealed class CustomerInventory(IDatabase database):ICustomerInventory
{
   // private readonly List<Customer> _customers = database.LoadCustomers(); // Laden der Daten

    public void InsertCustomer(Customer c)
    {
        database.Customers.Add(c);
        database.SaveChanges();
        //database.CreateCustomer(c);
        //_customers.Add(c);
       // database.SaveCustomers(_customers);
    }
    public List<Customer> ListCustomers()
    {

        return database.Customers.ToList();
    }

    

    public void DeleteCustomers()
    {
        database.Customers.RemoveRange(database.Customers);
        // _customers.Clear();
        //database.SaveCustomers(_customers);
    }
    

    public void DeleteCustomer(Customer c)
    {
        database.Customers.Remove(c);
    }

    public void UpdateCustomer(Customer c,String username, String passwordHash, String telefon)
    {
        c.Username = username;
        c.PasswordHash = passwordHash;
        c.Telefon = telefon;
        //database.SaveCustomers(_customers);
        database.SaveChanges();

        
    }
    public void UpdateCustomers()
    {
        database.SaveChanges();

    }

    

   


    
    
    
}