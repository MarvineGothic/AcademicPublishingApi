﻿using AcademicPublishingApi.Models;

namespace AcademicPublishingApi.Services
{
    public interface IResearchArticleRepository
    {
        Task<ResearchArticleDto?> GetResearchArticleById(int id);

        Task<IList<ResearchArticleDto>> GetArticlesByAuthorId(int authorId);

        Task<IList<ResearchArticleDto>> GetArticlesByJournalId(int journalId);
    }
}
