using Microsoft.EntityFrameworkCore;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Persistence;

namespace PimpYourBlech_ClassLibrary.Inventories.Implementation;

public sealed class CustomerInventory(IDatabase database) : ICustomerInventory
{
    public async Task InsertCustomerAsync(Customer c)
    {
        database.Customers.Add(c);
        await database.SaveChangesAsync();
        
    }
    
    public List<Customer> ListCustomers()
    {

        return database.Customers.ToList();
    }

    public async Task<List<Customer>> ListCustomersAsync()
    {
        return await database.Customers.ToListAsync();
    }
    
    
    public async Task<Customer> GetCustomerByIdIncludeAllAsync(int id)
    {

        return await database.Customers
            .Include(c => c.DeliveryAddresses)
            .Include(c => c.Orders)
            .Include(c => c.Configurations)
            .ThenInclude(cfg => cfg.Car)
            .Include(c => c.Configurations)
            .ThenInclude(cfg => cfg.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
    }    


    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        return await database.Customers.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Customer?> GetCustomerIncludeAdressesAsync(int id)
    {
        return await database.Customers.Include(c => c.DeliveryAddresses).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task DeleteCustomerAsync(Customer c)
    {
        database.Customers.Remove(c);
        await database.SaveChangesAsync();
    }


    public async Task UpdateCustomerAsync(Customer c)
    {
        var tracked = database.Customers.First(x => x.Id == c.Id);

        tracked.FirstName = c.FirstName;
        tracked.LastName = c.LastName;
        tracked.MailAddress = c.MailAddress;
        tracked.Telefon = c.Telefon;
        tracked.PasswordHash = c.PasswordHash;
        tracked.ImagePath = c.ImagePath; 
        tracked.AdminRights = c.AdminRights;
        tracked.DeliveryAddresses = c.DeliveryAddresses;
        await database.SaveChangesAsync();

        
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

    public List<Configuration> ListConfigurations()
    {
        return database.Configurations.ToList();
    }
    
    public async Task AddCommunityQuestionAsync(string content)
    {
        CommunityQuestion temp = new CommunityQuestion();
        temp.Content = content;
        temp.CreatedAt = DateTime.UtcNow;
        database.CommunityQuestions.Add(temp);
        database.SaveChanges();
    }

    public async Task AddCommunityAnswerAsync(int questionId, string content)
    {
        CommunityAnswer temp = new CommunityAnswer();
        temp.Content = content;
        temp.CreatedAt = DateTime.UtcNow;
        temp.QuestionId = questionId;
        database.CommunityAnswers.Add(temp);
        database.SaveChanges();
    }
    
    public async Task<List<CommunityQuestion>> GetCommunityQuestionsAsync()
    {
        return await database.CommunityQuestions
            .Include(q => q.Answers)
            .OrderByDescending(q => q.CreatedAt)
            .ToListAsync();
    }

    public void DeleteConfiguration(Configuration config)
    {
        database.Configurations.Remove(config);
        database.SaveChanges();
    }

    public void AddOrder(Order order)
    {
        database.Orders.Add(order);
        database.SaveChanges();
    }
    
    public async Task<List<Order>> GetOrdersAsync()
    {
        return await database.Orders
            .Include(o=>o.Customer)
            .Include(o=>o.DeliveryAddress)

            .ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        return await database.Orders.Include(o => o.Customer)
            .Include(o=> o.DeliveryAddress)
            .FirstOrDefaultAsync(o => o.OrderId == id); 
    }


    public void RemoveOrder(Order order)
    {
        database.Orders.Remove(order);
        database.SaveChanges();
    }
    
    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await database.Customers.AnyAsync(c => c.Username == username);
    }


    public async Task<Customer?> GetCustomerByUsernameAsync(string username)
    {
        return await database.Customers.FirstOrDefaultAsync(c => c.Username == username);
    }

    public async Task<DeliveryAddress> GetDeliveryAddressAsync(int id)
    {
        return await database.DeliveryAddresses.FirstOrDefaultAsync(d => d.Id == id);
    }
    
    public async Task<int> InsertDeliveryAddressAsync(DeliveryAddress address)
    {
        database.DeliveryAddresses.Add(address);
        await database.SaveChangesAsync();
        return address.Id;
    }

    public async Task<List<OrderPosition>> GetOrderItemsAsync(int id)
    {
        return await database.OrderPositions.Where(o => o.OrderId == id).ToListAsync();
    }
}

    

   


    
    
    
