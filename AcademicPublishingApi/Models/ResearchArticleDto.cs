namespace AcademicPublishingApi.Models
{
    public class ResearchArticleDto
    {
        public required int Id { get; set; }

        public required string Title { get; set; }

        public required string Abstract { get; set; }

        public required DateTime PublicationDate { get; set; }

        public required JournalDto Journal { get; set; }

        public IList<AuthorDto> Authors { get; set; } = [];
    }
}
