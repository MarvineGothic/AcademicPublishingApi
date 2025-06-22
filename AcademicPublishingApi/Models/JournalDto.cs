namespace AcademicPublishingApi.Models
{
    public class JournalDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string ISSN { get; set; }

        public required string Publisher { get; set; }
    }
}
