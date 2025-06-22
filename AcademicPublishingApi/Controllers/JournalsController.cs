using AcademicPublishingApi.Models;
using AcademicPublishingApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace AcademicPublishingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalsController(IResearchArticleRepository researchArticleService) : ControllerBase
    {
        private readonly IResearchArticleRepository _researchArticleService = researchArticleService;

        // GET: api/<JournalsController>/4/articles
        [HttpGet("{journalId:int}/articles")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResearchArticleDto>>> GetArticlesByJournalAsync([FromRoute] int journalId)
        {
            return Ok(await _researchArticleService.GetArticlesByJournalId(journalId));
        }
    }
}
