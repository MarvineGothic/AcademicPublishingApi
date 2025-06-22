using AcademicPublishingApi.Data.Entities;
using AcademicPublishingApi.Models;

namespace AcademicPublishingApi.Extensions
{
    public static class EntitiesExtensions
    {
        public static AuthorDto ToDto(this Author author)
        {
            return new AuthorDto()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Affiliation = author.Affiliation
            };
        }

        public static JournalDto ToDto(this Journal Journal)
        {
            return new JournalDto
            {
                Id = Journal.Id,
                Name = Journal.Name,
                ISSN = Journal.ISSN,
                Publisher = Journal.Publisher
            };
        }

        public static ResearchArticleDto ToDto(this ResearchArticle researchArticle)
        {
            return new ResearchArticleDto
            {
                Id = researchArticle.Id,
                Title = researchArticle.Title,
                Abstract = researchArticle.Abstract,
                PublicationDate = researchArticle.PublicationDate,
                Journal = researchArticle.Journal.ToDto(),
                Authors = [.. researchArticle.ArticleAuthors.Select(articleAuthor => articleAuthor.Author.ToDto())]
            };
        }
    }
}
