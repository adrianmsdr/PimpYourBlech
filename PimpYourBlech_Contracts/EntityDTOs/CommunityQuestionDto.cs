namespace PimpYourBlech_Contracts.EntityDTOs;

public class CommunityQuestionDto
{
     
    // Eindeutige ID der Frage
    public int Id { get; set; }
    
    // Inhalt der Frage
    public string Content { get; set; } = string.Empty;
    

    // Zeitpunkt der Erstellung der Frage
    public DateTime CreatedAt { get; set; }

    // Alle zugehörigen Antworten zur Frage
    public List<CommunityAnswerDto> Answers { get; set; } = new();

    }