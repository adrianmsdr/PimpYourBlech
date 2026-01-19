namespace PimpYourBlech_Contracts.EntityDTOs;

public class CommunityQuestionCreateDto
{
    // Inhalt der neu zu erstellenden Frage
    // Weitere Felder (Id, CreatedAt) werden serverseitig ergänzt
    public string Content { get; set; } = string.Empty;
}