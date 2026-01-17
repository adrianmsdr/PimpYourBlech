namespace PimpYourBlech_Data.Models;

public class CommunityQuestion
{
    
        public int Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<CommunityAnswer> Answers { get; set; } = new List<CommunityAnswer>();
    }

