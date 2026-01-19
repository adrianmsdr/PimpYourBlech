
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Data.Inventories;

namespace PimpYourBlech_ClassLibrary.Services.FAQ;

public class FaqService : IFaqService
{
    
    // Zugriff auf die Datenhaltung für Kunden- und Community-bezogene Daten
    private readonly ICustomerInventory customerInventory;

    public FaqService(ICustomerInventory customers)
    {
        customerInventory = customers;
    }
    
    public async Task<List<CommunityQuestionDto>> GetCommunityQuestionsAsync()
    {
        // Lädt alle Community-Fragen inklusive zugehöriger Antworten
        var questions = await customerInventory.GetCommunityQuestionsAsync();

        // Mapping von Datenbank-Entities auf DTOs für die UI
        return questions.Select(q => new CommunityQuestionDto
        {
            Id = q.Id,
            Content = q.Content,
            CreatedAt = q.CreatedAt,
            // Antworten werden explizit gemappt,
            // um keine Entity-Referenzen nach außen zu geben
            Answers = q.Answers.Select(a => new CommunityAnswerDto
            {
                Id = a.Id,
                Content = a.Content,
                CreatedAt = a.CreatedAt,
                QuestionId = a.QuestionId
            }).ToList()
        }).ToList();
    }

    
    // Füget Fragen hinzu 
    public async Task AddCommunityQuestionAsync(CommunityQuestionCreateDto dto)
    {
        await customerInventory.AddCommunityQuestionAsync(dto.Content);
    }

    //Fügt den Fragen Antworten hinzu
    public async Task AddCommunityAnswerAsync(CommunityAnswerCreateDto dto)
    {
        await customerInventory.AddCommunityAnswerAsync(dto.QuestionId, dto.Content);
    }
}