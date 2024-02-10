using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScienceArticles.Application.Dtos.SearchPublications;
using ScienceArticles.Application.Queries.GetArticlesFromService;
using ScienceArticles.Application.Services;

namespace ScienceArticles.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEuropePMCService _europePMCService;

        public ArticlesController(IMediator mediator, IEuropePMCService europePMCService)
        {
            _mediator = mediator;
            _europePMCService = europePMCService;
            
            

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

        [HttpGet("{id}")]

        public async Task<ActionResult<SearchPublicationsResponseItemDto>> GetArticlesFromService([FromRoute] string id)
        {
            try
            {
                var response = await _europePMCService.FindPublicationById(id);
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
