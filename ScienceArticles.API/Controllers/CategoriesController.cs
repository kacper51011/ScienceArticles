using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScienceArticles.Application.Dtos.GetCategories;
using ScienceArticles.Application.Queries.GetCategories;

namespace ScienceArticles.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint for getting categories, used when creating saved user articles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<CategoryResponeItem>>> GetCategories()
        {
            try
            {
                var query = new GetCategoriesQuery();

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
