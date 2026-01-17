namespace PimpYourBlech_Data.Models;

public class CommunityAnswer
{
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    // Foreign Key
    public int QuestionId { get; set; }

    // Navigation
    public CommunityQuestion Question { get; set; }
}
