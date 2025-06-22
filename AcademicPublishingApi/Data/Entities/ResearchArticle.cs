namespace AcademicPublishingApi.Data.Entities
{
    public class ResearchArticle
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Abstract { get; set; }
        public required DateTime PublicationDate { get; set; }

        public required int JournalId { get; set; }
        public Journal Journal { get; set; } = null!;

        public ICollection<ArticleAuthor> ArticleAuthors { get; set; } = [];
    }
}
