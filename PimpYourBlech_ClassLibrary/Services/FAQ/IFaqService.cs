namespace PimpYourBlech_ClassLibrary.Services.FAQ;
using PimpYourBlech_ClassLibrary.Entities;
public interface IFaqService
{
    Task<List<CommunityQuestion>> GetCommunityQuestionsAsync();
    Task AddCommunityQuestionAsync(string content);
    Task AddCommunityAnswerAsync(int questionId, string content);
}