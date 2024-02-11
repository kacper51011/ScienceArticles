using MediatR;
using ScienceArticles.Application.Dtos.SearchPublications;
using ScienceArticles.Application.Exceptions;
using ScienceArticles.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Queries.GetArticleFromServiceById
{
    public class GetArticleFromServiceByIdQueryHandler : IRequestHandler<GetArticleFromServiceByIdQuery, SearchPublicationsResponseItemDto>
    {
		private readonly IEuropePMCService _europePMCService;
        public GetArticleFromServiceByIdQueryHandler(IEuropePMCService europePMCService)
        {

            _europePMCService = europePMCService;
            
        }
        public async Task<SearchPublicationsResponseItemDto> Handle(GetArticleFromServiceByIdQuery request, CancellationToken cancellationToken)
        {
			try
			{
                var publication = await _europePMCService.FindPublicationById(request.publicationId);
                if (publication == null)
                {
                    throw new NotFoundException("Couldn`t find publication with specified Id in EuropePMC");
                }
                return publication;
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
