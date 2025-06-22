using AcademicPublishingApi.Data.Entities;

namespace AcademicPublishingApi.Migrations.InMemoryData
{
    public class SeedDataDto
    {
        public List<Author> Authors { get; set; } = [];
        public List<Journal> Journals { get; set; } = [];
        public List<ResearchArticle> ResearchArticles { get; set;} = [];
        public List<ArticleAuthor> ArticleAuthors { get; set; } = [];
    }
}
