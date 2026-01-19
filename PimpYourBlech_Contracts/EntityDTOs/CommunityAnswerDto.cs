namespace PimpYourBlech_Contracts.EntityDTOs;

public class CommunityAnswerDto
{
    
    // Eindeutige ID der Antwort (aus der Datenbank)
    public int Id { get; set; }

    // Gespeicherter Inhalt der Antwort
    public string Content { get; set; } = string.Empty;

    // Zeitpunkt der Erstellung, wird serverseitig gesetzt
    public DateTime CreatedAt { get; set; }

    // Foreign Key für die zuordnung zur Frage
    public int QuestionId { get; set; }

    
}