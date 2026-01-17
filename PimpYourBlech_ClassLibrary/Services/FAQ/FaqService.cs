
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
    
    public async Task<List<CommunityQuestionDto>> GetCommunityQuestionsAsync()
    {
        var questions = await customerInventory.GetCommunityQuestionsAsync();

        return questions.Select(q => new CommunityQuestionDto
        {
            Id = q.Id,
            Content = q.Content,
            CreatedAt = q.CreatedAt,
            Answers = q.Answers.Select(a => new CommunityAnswerDto
            {
                Id = a.Id,
                Content = a.Content,
                CreatedAt = a.CreatedAt,
                QuestionId = a.QuestionId
            }).ToList()
        }).ToList();
    }

    public async Task AddCommunityQuestionAsync(CommunityQuestionCreateDto dto)
    {
        await customerInventory.AddCommunityQuestionAsync(dto.Content);
    }

    public async Task AddCommunityAnswerAsync(CommunityAnswerCreateDto dto)
    {
        await customerInventory.AddCommunityAnswerAsync(dto.QuestionId, dto.Content);
    }
}