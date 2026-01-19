
using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.Services.FAQ;
public interface IFaqService
{
    // Liefert alle Community-Fragen inklusive Antworten
    Task<List<CommunityQuestionDto>> GetCommunityQuestionsAsync();
    
    // Erstellt eine neue Community-Frage
    Task AddCommunityQuestionAsync(CommunityQuestionCreateDto dto);
    
    // Fügt eine Antwort zu einer bestehenden Frage hinzu
    Task AddCommunityAnswerAsync(CommunityAnswerCreateDto dto);
}