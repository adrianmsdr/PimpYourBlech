namespace PimpYourBlech_Contracts.EntityDTOs;

public class CommunityAnswerDto
{
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    // Foreign Key
    public int QuestionId { get; set; }

    
}