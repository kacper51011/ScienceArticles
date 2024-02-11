using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens.Saml;
using ScienceArticles.Application.Exceptions;
using ScienceArticles.Domain.Interfaces;
using ScienceArticles.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Commands.DeleteSavedUserArticle
{
    public class DeleteSavedUserArticleCommandHandler : IRequestHandler<DeleteSavedUserArticleCommand>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IArticleRepository _articleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSavedUserArticleCommandHandler(IHttpContextAccessor contextAccessor, IArticleRepository articleRepository, IUnitOfWork unitOfWork)
        {
            _contextAccessor = contextAccessor;
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteSavedUserArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var articleToDelete = await _articleRepository.GetArticleById(new ArticleId(Guid.Parse(request.articleId)));
                if (articleToDelete == null)
                {
                    throw new ArgumentNotValidException("Cannot find that article");
                }

                else if (userId != articleToDelete.UserId.Value.ToString())
                {
                    throw new UnauthorizedException("You are not allowed to do this action, saved article belongs to someone else");
                }

                await _articleRepository.DeleteSavedArticle(articleToDelete);

                return;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
