
using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.Services.FAQ;
public interface IFaqService
{
    Task<List<CommunityQuestionDto>>? GetCommunityQuestionsAsync();
    Task AddCommunityQuestionAsync(string content);
    Task AddCommunityAnswerAsync(int questionId, string content);
}