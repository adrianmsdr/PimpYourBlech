namespace PimpYourBlech_Contracts.EntityDTOs;

public class CommunityQuestionDto
{
     
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public List<CommunityAnswerDto> Answers { get; set; } = new();

    }