using Microsoft.EntityFrameworkCore;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Persistence;

namespace PimpYourBlech_ClassLibrary.Inventories.Implementation;

public sealed class CustomerInventory(IDatabase database):ICustomerInventory
{
    public void InsertCustomer(Customer c)
    {
        database.Customers.Add(c);
        database.SaveChanges();
        
    }
    public List<Customer> ListCustomers()
    {

        return database.Customers.ToList();
    }

    

    public void DeleteCustomers()
    {
        database.Customers.RemoveRange(database.Customers);
        database.SaveChanges();
        // _customers.Clear();
        //database.SaveCustomers(_customers);
    }
    

    public void DeleteCustomer(Customer c)
    {
        database.Customers.Remove(c);
        database.SaveChanges();
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

    public void AddConfiguration(Configuration config)
    {
        database.Configurations.Add(config);
        database.SaveChanges();
    }
    
    public List<Configuration> GetConfigurationsForCustomer(int customerId)
    {
        return database.Configurations
            .Where(c => c.CustomerId == customerId)
            .Include(cfg => cfg.Car)        // wichtig!
            .Include(cfg => cfg.Products)   // falls du Products brauchst
            .ToList();
    }
    }

    

   


    
    
    
