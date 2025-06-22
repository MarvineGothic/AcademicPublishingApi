namespace AcademicPublishingApi.Data.Entities
{
    public class ArticleAuthor
    {
        public required int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        public required int ResearchArticleId { get; set; }
        public ResearchArticle ResearchArticle { get; set; } = null!;
    }
}