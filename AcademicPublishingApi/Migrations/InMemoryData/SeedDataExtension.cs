using AcademicPublishingApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AcademicPublishingApi.Migrations.InMemoryData
{
    public static class SeedDataExtension
    {
        public static void SeedInMemoryData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();

            Console.WriteLine("Seeding in-memory database...");
            Console.WriteLine($"Database contains {db.ResearchArticles.Count()} articles.");

            // Prevent re-seeding
            if (!db.Database.IsInMemory() || db.ResearchArticles.Any())
            {
                return;
            }

            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Migrations", "InMemoryData", "seed-data.json");
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

            db.Journals.AddRange(seedData.Journals);
            db.Authors.AddRange(seedData.Authors);
            db.ResearchArticles.AddRange(seedData.ResearchArticles);
            db.ArticleAuthors.AddRange(seedData.ArticleAuthors);

            db.SaveChanges();
        }
    }
}
