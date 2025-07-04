﻿using AcademicPublishingApi.Models;
using AcademicPublishingApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace AcademicPublishingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResearchArticlesController(IResearchArticleRepository researchArticleRepository) : ControllerBase
    {
        private readonly IResearchArticleRepository _researchArticleRepository = researchArticleRepository;

        // GET article by ID
        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResearchArticleDto>> GetArticle([FromRoute] int id)
        {
            var article = await _researchArticleRepository.GetResearchArticleById(id);
            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }
    }
}
