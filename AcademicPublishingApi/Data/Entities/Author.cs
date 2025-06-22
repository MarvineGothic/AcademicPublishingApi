namespace AcademicPublishingApi.Data.Entities
{
    public class Author
    {
        public required int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Affiliation { get; set; }

        public ICollection<ArticleAuthor> ArticleAuthors { get; set; } = [];
    }
}
