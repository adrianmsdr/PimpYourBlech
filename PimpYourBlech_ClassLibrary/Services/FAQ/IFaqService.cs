
using PimpYourBlech_Contracts.EntityDTOs;

namespace PimpYourBlech_ClassLibrary.Services.FAQ;
public interface IFaqService
{
    Task<List<CommunityQuestionDto>> GetCommunityQuestionsAsync();
    Task AddCommunityQuestionAsync(CommunityQuestionCreateDto dto);
    Task AddCommunityAnswerAsync(CommunityAnswerCreateDto dto);
}