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
}

    

   


    
    
    
