using AcademicPublishingApi.Data;
using AcademicPublishingApi.Data.Entities;
using AcademicPublishingApi.Services;
using Microsoft.EntityFrameworkCore;

namespace AcademicPublishingApi.Tests;

public class ArticleServiceTests
{
    private readonly AppDbContext _dbContext;
    private readonly ResearchArticleRepository _article_service;

    public ArticleServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb_" + Guid.NewGuid().ToString())
            .Options;

        _dbContext = new AppDbContext(options);

        var journal = new Journal { Id = 1, Name = "Test Journal", ISSN = "1234-5678", Publisher = "TestPub" };
        var author = new Author { Id = 1, FirstName = "Test", LastName = "Author", Affiliation = "Test University" };
        IList<ResearchArticle> articles = [
            new ResearchArticle { Id = 1, Title = "Test1 Article", Abstract = "Abstract1", JournalId = 1, PublicationDate = DateTime.UtcNow },
            new ResearchArticle { Id = 2, Title = "Test2 Article", Abstract = "Abstract2", JournalId = 1, PublicationDate = DateTime.UtcNow }
            ];
        IList<ArticleAuthor> articleAuthors = [
            new ArticleAuthor { AuthorId = 1, ResearchArticleId = 1 },
            new ArticleAuthor { AuthorId = 1, ResearchArticleId = 2 }
        ];

        _dbContext.Journals.Add(journal);
        _dbContext.Authors.Add(author);
        _dbContext.ResearchArticles.AddRange(articles);
        _dbContext.ArticleAuthors.AddRange(articleAuthors);

        _dbContext.SaveChanges();

        _article_service = new ResearchArticleRepository(_dbContext);
    }

    [Fact]
    public async Task GetResearchArticleById_ReturnsNull()
    {
        var article = await _article_service.GetResearchArticleById(200);

        Assert.Null(article);
    }

    [Fact]
    public async Task GetArticlesByAuthorId_ReturnsEmptyList()
    {
        var articles = await _article_service.GetArticlesByAuthorId(2);

        Assert.NotNull(articles);
        Assert.Empty(articles);
    }

    [Fact]
    public async Task GetArticlesByJournalId_ReturnsEmptyList()
    {
        var articles = await _article_service.GetArticlesByJournalId(2);

        Assert.NotNull(articles);
        Assert.Empty(articles);
    }

    [Fact]
    public async Task GetResearchArticleById_ReturnsArticle()
    {
        var article = await _article_service.GetResearchArticleById(1);

        Assert.NotNull(article);
        Assert.Equal("Test1 Article", article!.Title);
    }

    [Fact]
    public async Task GetArticlesByAuthorId_ReturnsCorrectArticles()
    {
        var articles = await _article_service.GetArticlesByAuthorId(1);

        Assert.Equal(2, articles.Count);
        Assert.Equal("Test1 Article", articles.First().Title);
    }

    [Fact]
    public async Task GetArticlesByJournalId_ReturnsCorrectArticles()
    {
        var articles = await _article_service.GetArticlesByJournalId(1);
        Assert.Equal(2, articles.Count);
        Assert.Equal("Test1 Article", articles.First().Title);
    }
}
