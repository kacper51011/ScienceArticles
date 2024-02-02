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

                var data = response.@return.resultList;

                if (data == null)
                {
                    new ArgumentNullException(nameof(data));
                }

                var dtoList = new List<SearchPublicationsResponseItemDto>();
                foreach (result result in data)
                {
                    var item = new SearchPublicationsResponseItemDto();
                    item.Abstract = result.abstractText;

                    item.ServiceId = result.id;
                    item.Title = result.title;
                    item.PublicationYear = result.pubYear;
                    item.FullText = result.fullText;

                    var authors = new List<string>();
                    if (result.authorIdList != null)
                    {
                        foreach (var authorItem in result.authorList)
                        {
                            authors.Add(authorItem.firstName + " " + authorItem.lastName);
                        }
                    }

                    item.Authors = authors;

                    dtoList.Add(item);
                }
                return dtoList;
            }
            catch (Exception)
            {

                throw;
            }



        }
    }
}
