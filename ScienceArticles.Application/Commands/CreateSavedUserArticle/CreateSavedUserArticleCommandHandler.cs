using MediatR;
using Microsoft.AspNetCore.Http;
using ScienceArticles.Application.Services;
using ScienceArticles.Domain.Entities;
using ScienceArticles.Domain.Interfaces;
using ScienceArticles.Domain.ValueObjects;
using System.Security.Claims;

namespace ScienceArticles.Application.Commands.CreateSavedUserArticle
{
    public class CreateSavedUserArticleCommandHandler : IRequestHandler<CreateSavedUserArticleCommand>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IArticleRepository _articleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEuropePMCService _europePMCService;

        public CreateSavedUserArticleCommandHandler(IHttpContextAccessor contextAccessor, IArticleRepository articleRepository, IEuropePMCService europePMCService, IUnitOfWork unitOfWork)
        {
            _contextAccessor = contextAccessor;
            _articleRepository = articleRepository;
            _europePMCService = europePMCService;
            _unitOfWork = unitOfWork;



        }
        public async Task Handle(CreateSavedUserArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    throw new ArgumentNullException("UserId not specified");
                }

                var publication = await _europePMCService.FindPublicationById(request.dto.PublicationId);
                if (publication == null)
                {
                    throw new ArgumentNullException("couldn`t find specified publication");
                }

                var ArticleToSave = Article.Create(publication.PublicationId, publication.Title, publication.PublicationYear, publication.Abstract, publication.TextLink, new UserId(Guid.Parse(userId)) , new CategoryId(Guid.Parse(request.dto.CategoryId)));

                await _articleRepository.SaveArticle(ArticleToSave);
                await _unitOfWork.SaveChangesAsync();

                return;
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
