namespace AcademicPublishingApi.Data.Entities
{
    public class Journal
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string ISSN { get; set; }
        public required string Publisher { get; set; }

        public ICollection<ResearchArticle> Articles { get; set; } = [];
    }
}
