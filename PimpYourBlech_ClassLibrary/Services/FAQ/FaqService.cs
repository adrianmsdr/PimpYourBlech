
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Data.Inventories;

namespace PimpYourBlech_ClassLibrary.Services.FAQ;

public class FaqService : IFaqService
{
    private readonly ICustomerInventory customerInventory;

    public FaqService(ICustomerInventory customers)
    {
        customerInventory = customers;
    }
    
    public async Task<List<CommunityQuestionDto>>? GetCommunityQuestionsAsync()
    {
        var communityQuestions = await customerInventory.GetCommunityQuestionsAsync();;
       return communityQuestions.ConvertAll(q => new CommunityQuestionDto()
        {
           Id = q.Id,
           Content = q.Content,
           CreatedAt = q.CreatedAt,
        });
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