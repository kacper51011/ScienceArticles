using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScienceArticles.Application.Dtos.SearchPublications;
using ScienceArticles.Application.Exceptions;
using ScienceArticles.Application.Queries.GetArticleFromServiceById;
using ScienceArticles.Application.Queries.GetArticlesFromService;
using ScienceArticles.Application.Services;

namespace ScienceArticles.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArticlesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Responsible for getting specified number of publications, coming from EuropePMC SOAP web service
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<SearchPublicationsResponseItemDto>>> GetArticlesFromService([FromQuery]SearchPublicationsRequestDto dto)
        {
            try
            {
                var query = new GetArticlesFromServiceQuery(dto);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Responsible for getting one single publication from EuropePMC SOAP web service with specified ID
        /// </summary>
        /// <param name="publicationId"></param>
        /// <returns></returns>
        [HttpGet("{publicationId}")]
        public async Task<ActionResult<SearchPublicationsResponseItemDto>> GetArticleFromServiceByPublicationId([FromRoute] string publicationId)
        {
            try
            {
                var query = new GetArticleFromServiceByIdQuery(publicationId);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {

                return StatusCode(404, ex.Message);
            }

            catch (Exception)
            {

                throw;
            }
        }

    }
}
