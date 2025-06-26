using AcademicPublishingApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcademicPublishingApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Journal> Journals => Set<Journal>();
    public DbSet<ResearchArticle> ResearchArticles => Set<ResearchArticle>();
    public DbSet<ArticleAuthor> ArticleAuthors => Set<ArticleAuthor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ArticleAuthor>()
            .HasKey(aa => new { aa.AuthorId, aa.ResearchArticleId });

        modelBuilder.Entity<ArticleAuthor>()
            .HasOne(aa => aa.Author)
            .WithMany(a => a.ArticleAuthors)
            .HasForeignKey(aa => aa.AuthorId)
            .HasPrincipalKey(a => a.Id);

        modelBuilder.Entity<ArticleAuthor>()
            .HasOne(aa => aa.ResearchArticle)
            .WithMany(ra => ra.ArticleAuthors)
            .HasForeignKey(aa => aa.ResearchArticleId)
            .HasPrincipalKey(ra => ra.Id);

        modelBuilder.Entity<ResearchArticle>()
            .HasOne(a => a.Journal)
            .WithMany(j => j.Articles)
            .HasForeignKey(a => a.JournalId)
            .HasPrincipalKey(j => j.Id);
    }
}
