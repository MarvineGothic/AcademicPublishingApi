using AcademicPublishingApi.Models;
using AcademicPublishingApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace AcademicPublishingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalsController(IResearchArticleRepository researchArticleRepository) : ControllerBase
    {
        private readonly IResearchArticleRepository _researchArticleRepository = researchArticleRepository;

        // GET: api/<JournalsController>/4/articles
        [HttpGet("{journalId:int}/articles")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResearchArticleDto>>> GetArticlesByJournalAsync([FromRoute] int journalId)
        {
            return Ok(await _researchArticleRepository.GetArticlesByJournalId(journalId));
        }
    }
}
