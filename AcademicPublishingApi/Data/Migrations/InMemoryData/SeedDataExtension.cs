using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AcademicPublishingApi.Data.Migrations.InMemoryData
{
    public static class SeedDataExtension
    {
        public static async Task SeedInMemoryData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated();

            Console.WriteLine("Seeding in-memory database...");
            Console.WriteLine($"Database contains {dbContext.ResearchArticles.Count()} articles.");

            // Prevent re-seeding
            if (!dbContext.Database.IsInMemory() || await dbContext.ResearchArticles.AnyAsync())
            {
                return;
            }

            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Migrations", "InMemoryData", "seed-data.json");
            if (!File.Exists(jsonPath))
            {
                throw new FileNotFoundException("Seed file not found", jsonPath);
            }

            var json = File.ReadAllText(jsonPath);
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var seedData = JsonSerializer.Deserialize<SeedDataDto>(json, jsonOptions) ?? throw new Exception("No seed data!");

            dbContext.Journals.AddRange(seedData.Journals);
            dbContext.Authors.AddRange(seedData.Authors);
            dbContext.ResearchArticles.AddRange(seedData.ResearchArticles);
            dbContext.ArticleAuthors.AddRange(seedData.ArticleAuthors);

            await dbContext.SaveChangesAsync();
        }
    }
}
