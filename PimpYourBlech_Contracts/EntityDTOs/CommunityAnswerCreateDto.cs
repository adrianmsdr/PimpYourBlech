namespace PimpYourBlech_Contracts.EntityDTOs;

public class CommunityAnswerCreateDto
{
    
    // Referenz auf die Frage, zu der diese Antwort gehört
    // Wird beim Erstellen einer neuen Antwort benötigt

    public int QuestionId { get; set; }
    
    // Inhalt der Antwort, wie er vom Benutzer eingegeben wird
    // Enthält keine Metadaten (Id, Zeitstempel), da diese vom System gesetzt werden
    public string Content { get; set; } = string.Empty;
}