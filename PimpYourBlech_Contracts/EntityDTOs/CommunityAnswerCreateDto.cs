namespace PimpYourBlech_Contracts.EntityDTOs;

public class CommunityAnswerCreateDto
{
    public int QuestionId { get; set; }

    public string Content { get; set; } = string.Empty;
}