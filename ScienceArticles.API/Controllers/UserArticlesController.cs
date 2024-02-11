using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScienceArticles.Application.Commands.CreateSavedUserArticle;
using ScienceArticles.Application.Commands.DeleteSavedUserArticle;
using ScienceArticles.Application.Dtos.CreateSavedUserArticle;
using ScienceArticles.Application.Dtos.GetUserArticles;
using ScienceArticles.Application.Exceptions;
using ScienceArticles.Application.Queries.GetSavedUserArticles;

namespace ScienceArticles.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserArticlesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserArticlesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint responsible for getting every article saved by a user. Requires Authentication
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetUserArticlesResponseDto>>> GetUserArticles()
        {
            try
            {
                var query = new GetSavedUserArticlesQuery();
                var response = await _mediator.Send(query);

                return Ok(response);
            }
            catch (NotFoundException ex)
            {

                return StatusCode(404, ex.Message);
            }
            catch (UnauthorizedException ex)
            {

                return StatusCode(401, ex.Message);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Responsible for saving publication for user, based on publicationId, with specified, choosen by user category. Requires Authentication
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SaveUserArticle(CreateSavedUserRequestDto dto)
        {
            try
            {
                var command = new CreateSavedUserArticleCommand(dto);
                await _mediator.Send(command);

                return Ok();
            }
            catch (UnauthorizedException ex)
            {

                return StatusCode(401, ex.Message);
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
        /// Responsible for deleting saved article, requires Authentication
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>

        [HttpDelete("{articleId}")]

        public async Task<ActionResult> DeleteUserArticle([FromRoute] string articleId)
        {

            try
            {
                var command = new DeleteSavedUserArticleCommand(articleId);
                await _mediator.Send(command);

                return Ok();
            }
            catch (UnauthorizedException ex)
            {

                return StatusCode(401, ex.Message);
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
