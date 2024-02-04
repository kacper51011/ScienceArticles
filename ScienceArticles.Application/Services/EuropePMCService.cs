using EuropePMCServiceConnection;
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

        public async Task<SearchPublicationsResponseItemDto> FindPublicationById(string publicationId)
        {
            try
            {

                var response = await _client.searchPublicationsAsync(publicationId, "core", "*", null, null, null, null);

                var data = response.@return.resultList;

                if (data == null)
                {
                    new ArgumentNullException(nameof(data));
                }

                var item = new SearchPublicationsResponseItemDto();
                item.Abstract = data[0].abstractText;
                item.PublicationId = data[0].id;
                item.Title = data[0].title;
                item.PublicationYear = data[0].pubYear;
                item.TextLink = data[0].fullTextUrlList[0].url;
                var authors = new List<string>();

                if (data[0].authorIdList != null)
                {
                    foreach (var authorItem in data[0].authorList)
                    {
                        authors.Add(authorItem.firstName + " " + authorItem.lastName);
                    }
                }
                item.Authors = authors;
                return item;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<SearchPublicationsResponseItemDto>> FindPublicationsAsync(SearchPublicationsRequestDto dto)
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

                    item.PublicationId = result.id;
                    item.Title = result.title;
                    item.PublicationYear = result.pubYear;

                    if (result.fullTextUrlList != null)
                    {
                        item.TextLink = result.fullTextUrlList[0].url;
                    }


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
