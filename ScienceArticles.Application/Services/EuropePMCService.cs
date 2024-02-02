using EuropePMCServiceConnection;
using ScienceArticles.Application.Dtos.GetArticleText;
using ScienceArticles.Application.Dtos.SearchPublications;

namespace ScienceArticles.Application.Services
{
    public class EuropePMCService : IEuropePMCService
    {
        private readonly WSCitationImplClient _client;
        public EuropePMCService()
        {
            _client = new WSCitationImplClient();
        }
        public Task<GetArticleTextResponseDto> FulltextXMLAsync(GetArticleTextRequestDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SearchPublicationsResponseItemDto>> SearchPublicationsAsync(SearchPublicationsRequestDto dto)
        {
            try
            {
                var response = await _client.searchPublicationsAsync(dto.QueryString, "core", "*", dto.PageSize.ToString(), null, dto.SynonymsIncluded.ToString().ToLower(), null);
                responseWrapper wrapper = new responseWrapper();

                var data = response.@return.resultList;

                var dtoList = new List<SearchPublicationsResponseItemDto>();
                //foreach(result result in data)
                //{
                //    result.
                //}
            }
            catch (Exception)
            {

                throw;
            }



        }
    }
}
