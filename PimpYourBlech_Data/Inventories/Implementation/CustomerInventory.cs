using Microsoft.EntityFrameworkCore;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Enums;
using PimpYourBlech_Contracts.Query;
using PimpYourBlech_Data.Models;
using PimpYourBlech_Data.Persistence;

namespace PimpYourBlech_Data.Inventories.Implementation;

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
    
    public async Task<List<Configuration>> GetConfigurationsForCustomer(int customerId)
    {
        return await database.Configurations
            .Where(c => c.CustomerId == customerId)
            .Include(cfg => cfg.Car)        // wichtig!
            .Include(cfg => cfg.Products)   // falls du Products brauchst
            .ToListAsync();
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
        return await database.DeliveryAddresses.
            FirstOrDefaultAsync(d => d.Id == id);
    }
    
    public async Task<int> InsertDeliveryAddressAsync(DeliveryAddress address)
    {
        database.DeliveryAddresses.Add(address);
        await database.SaveChangesAsync();
        return address.Id;
    }

    public async Task<int> InsertPaymentValueAsync(PaymentValue paymentValue)
    {
        database.PaymentValues.Add(paymentValue);
        await database.SaveChangesAsync();
        return paymentValue.Id;
    }


    public async Task<List<OrderPosition>> GetOrderItemsAsync(int id)
    {
        return await database.OrderPositions.Where(o => o.OrderId == id).ToListAsync();
    }

    public async Task<PaymentValue?> GetPaymentValueAsync(int id)
    {
        return await database.PaymentValues.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<PaymentValue>> GetPaymentValuesAsync(int id)
    {
        return await database.PaymentValues.Where(p => p.CustomerId == id).ToListAsync();
    }

    public async Task<List<Customer>> QueryCustomersAsync(CustomerListQuery q)
    {
        
        IQueryable<Customer> query = database.Customers.AsNoTracking();

        if (q.CustomerId is not null)
            query = query.Where(c => c.Id == q.CustomerId.Value);
  
        if (!string.IsNullOrWhiteSpace(q.SearchTerm))
        {
            var term = q.SearchTerm.Trim();
            query = query.Where(c =>
                (c.FirstName != null && c.FirstName.Contains(term)) ||
                (c.LastName  != null && c.LastName.Contains(term)) ||
                (c.Username  != null && c.Username.Contains(term)) ||
                (c.MailAddress != null && c.MailAddress.Contains(term))
            );
        }
        
        if (!string.IsNullOrWhiteSpace(q.PhoneContains))
            query = query.Where(c => c.Telefon == q.PhoneContains);

        query = q.SortBy switch
        {
            CustomerSort.NameDesc  => query.OrderByDescending(p => p.Username),
            CustomerSort.NameAsc   => query.OrderBy(p => p.Username),
        };

        return await query.ToListAsync();
    }

    public async Task<List<DeliveryAddress>> GetUserAddressesAsync(int customerId)
    {
        return await database.DeliveryAddresses.Where(d => d.CustomerId == customerId).ToListAsync();
    }
}

    

   


    
    
    
