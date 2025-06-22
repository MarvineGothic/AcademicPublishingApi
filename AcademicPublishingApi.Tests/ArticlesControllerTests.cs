using AcademicPublishingApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.OpenApi.Extensions;
using System.Text.Json;

namespace AcademicPublishingApi.Tests
{
    public class ArticlesControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        [Fact]
        public async Task GetArticleById_Returns404()
        {
            var response = await _client.GetAsync("/api/researcharticles/100");
            Assert.Equal("NotFound", response.StatusCode.GetDisplayName());
        }

        [Fact]
        public async Task GetArticleById_ReturnsArticle()
        {
            var response = await _client.GetAsync("/api/researcharticles/1");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var article = JsonSerializer.Deserialize<ResearchArticleDto>(json, _jsonOptions);

            Assert.Equal("AI in Healthcare", article?.Title);
            Assert.Equal("AI Journal", article?.Journal.Name);
            Assert.Equal(2, article?.Authors.Count);
        }

        [Fact]
        public async Task GetArticlesByAuthor_ReturnsEmptyList()
        {
            var response = await _client.GetAsync("/api/authors/100/articles");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var articles = JsonSerializer.Deserialize<IList<ResearchArticleDto>>(json, _jsonOptions);

            Assert.NotNull(articles);
            Assert.Empty(articles);
        }

        [Fact]
        public async Task GetArticlesByAuthor_ReturnsArticles()
        {
            var response = await _client.GetAsync("/api/authors/1/articles");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var articles = JsonSerializer.Deserialize<IList<ResearchArticleDto>>(json, _jsonOptions);

            Assert.NotNull(articles);
            Assert.Equal(3, articles.Count);
            Assert.Contains(articles, a => a.Title == "AI in Healthcare");
            Assert.Contains(articles, a => a.Title == "IoT Data Processing");  
            Assert.Contains(articles, a => a.Title == "Emotion Recognition AI");
        }

        [Fact]
        public async Task GetArticlesByJournal_ReturnsEmptyList()
        {
            var response = await _client.GetAsync("/api/journals/100/articles");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var articles = JsonSerializer.Deserialize<IList<ResearchArticleDto>>(json, _jsonOptions);

            Assert.NotNull(articles);
            Assert.Empty(articles);
        }

        [Fact]
        public async Task GetArticlesByJournal_ReturnsArticles()
        {
            var response = await _client.GetAsync("/api/journals/1/articles");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var articles = JsonSerializer.Deserialize<IList<ResearchArticleDto>>(json, _jsonOptions);

            Assert.NotNull(articles);
            Assert.Equal(6, articles.Count);
            Assert.Contains("AI in Healthcare", json);
            Assert.Contains("Ethics in AI", json);
            Assert.Contains("Reinforcement Learning Agents", json);
            Assert.Contains("Bias in Machine Learning", json);
            Assert.Contains("AI for Wildlife Conservation", json);
            Assert.Contains("AI for Language Learning", json);
        }
    }
}
