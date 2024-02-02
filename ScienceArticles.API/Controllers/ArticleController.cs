using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScienceArticles.Application.Dtos.SearchPublications;
using ScienceArticles.Application.Queries.GetArticlesFromService;

namespace ScienceArticles.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<SearchPublicationsResponseItemDto>>> GetArticlesFromService([FromQuery]SearchPublicationsRequestDto dto)
        {
            try
            {
                var query = new GetArticlesFromServiceQuery(dto);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
