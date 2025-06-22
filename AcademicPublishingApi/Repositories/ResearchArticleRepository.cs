using AcademicPublishingApi.Data;
using AcademicPublishingApi.Extensions;
using AcademicPublishingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademicPublishingApi.Services
{
    public class ResearchArticleRepository(AppDbContext appDbContext) : IResearchArticleRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<ResearchArticleDto?> GetResearchArticleById(int id)
        {
            var article = await _appDbContext.ResearchArticles
                .Include(researchArticle => researchArticle.Journal)
                .Include(researchArticle => researchArticle.ArticleAuthors)
                .ThenInclude(articleAuthor => articleAuthor.Author)
                .FirstOrDefaultAsync(researchArticle => researchArticle.Id == id);

            return article?.ToDto();
        }

        public async Task<IList<ResearchArticleDto>> GetArticlesByAuthorId(int authorId)
        {
            return await _appDbContext.ArticleAuthors
                .Include(articleAuthor => articleAuthor.ResearchArticle.Journal)
                .Include(articleAuthor => articleAuthor.ResearchArticle.ArticleAuthors)
                .ThenInclude(articleAuthor => articleAuthor.Author)
                .Where(articleAuthor => articleAuthor.AuthorId == authorId)
                .Select(articleAuthor => articleAuthor.ResearchArticle.ToDto())
                .ToListAsync();
        }

        public async Task<IList<ResearchArticleDto>> GetArticlesByJournalId(int journalId)
        {
            return await _appDbContext.ResearchArticles
                .Include(researchArticle => researchArticle.Journal)
                .Include(researchArticle => researchArticle.ArticleAuthors)
                .ThenInclude(articleAuthor => articleAuthor.Author)
                .Where(researchArticle => researchArticle.JournalId == journalId)
                .Select(researchArticle => researchArticle.ToDto())
                .ToListAsync();
        }
    }
}
