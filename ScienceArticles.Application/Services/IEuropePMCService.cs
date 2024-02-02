using EuropePMCServiceConnection;
using ScienceArticles.Application.Dtos.GetArticleText;
using ScienceArticles.Application.Dtos.SearchPublications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Services
{
    public interface IEuropePMCService
    {
        public Task<List<SearchPublicationsResponseItemDto>> SearchPublicationsAsync(SearchPublicationsRequestDto dto);
        public Task<GetArticleTextResponseDto> FulltextXMLAsync(GetArticleTextRequestDto dto);

    }
}
