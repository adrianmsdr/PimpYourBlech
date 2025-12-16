using Microsoft.EntityFrameworkCore;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Inventories;
using PimpYourBlech_ClassLibrary.Persistence.EFDatabase;

namespace PimpYourBlech_ClassLibrary.Services.FAQ;

public class FaqService : IFaqService
{
    private readonly ICustomerInventory customerInventory;

    public FaqService(ICustomerInventory customers)
    {
        customerInventory = customers;
    }
    
    public async Task<List<CommunityQuestion>> GetCommunityQuestionsAsync()
    {
        return await customerInventory.GetCommunityQuestionsAsync();
    }

    public async Task AddCommunityQuestionAsync(string content)
    {
        await customerInventory.AddCommunityQuestionAsync(content);
    }

    public async Task AddCommunityAnswerAsync(int questionId, string content)
    {
        await customerInventory.AddCommunityAnswerAsync(questionId, content);
    }
}