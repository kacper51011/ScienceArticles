using MediatR;
using Microsoft.AspNetCore.Http;
using ScienceArticles.Application.Exceptions;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEuropePMCService _europePMCService;

        public CreateSavedUserArticleCommandHandler(IHttpContextAccessor contextAccessor, IArticleRepository articleRepository, IEuropePMCService europePMCService, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _contextAccessor = contextAccessor;
            _articleRepository = articleRepository;
            _europePMCService = europePMCService;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }
        public async Task Handle(CreateSavedUserArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    throw new UnauthorizedException("UserId not specified or incorrect");
                }

                var publication = await _europePMCService.FindPublicationById(request.dto.PublicationId);
                if (publication == null)
                {
                    throw new NotFoundException("Couldn`t find specified publication");
                }

                var category = await _categoryRepository.GetCategoryById(new CategoryId(Guid.Parse(request.dto.CategoryId)));
                if (category == null)
                {
                    throw new NotFoundException("Couldn`t find specified category");
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
