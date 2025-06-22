using AcademicPublishingApi.Models;
using AcademicPublishingApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace AcademicPublishingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController(IResearchArticleService researchArticleService) : ControllerBase
    {
        private readonly IResearchArticleService _researchArticleService = researchArticleService;

        // GET api/<AuthorController>/5/articles
        [HttpGet("{authorId:int}/articles")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResearchArticleDto>>> GetArticlesByAuthorAsync([FromRoute] int authorId)
        {
            return Ok(await _researchArticleService.GetArticlesByAuthorId(authorId));
        }
    }
}
