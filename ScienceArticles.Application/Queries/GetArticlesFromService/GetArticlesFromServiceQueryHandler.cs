using MediatR;
using ScienceArticles.Application.Dtos.SearchPublications;
using ScienceArticles.Application.Exceptions;
using ScienceArticles.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Queries.GetArticlesFromService
{
    public class GetArticlesFromServiceQueryHandler : IRequestHandler<GetArticlesFromServiceQuery, List<SearchPublicationsResponseItemDto>>
    {
        private readonly IEuropePMCService _service;

        public GetArticlesFromServiceQueryHandler(IEuropePMCService service)
        {
            _service = service;
        }

        public async Task<List<SearchPublicationsResponseItemDto>> Handle(GetArticlesFromServiceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.FindPublicationsAsync(request.dto);
                if(response == null ||  response.Count == 0)
                {
                    throw new NotFoundException("Couldn`t find publications for query");
                }

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
