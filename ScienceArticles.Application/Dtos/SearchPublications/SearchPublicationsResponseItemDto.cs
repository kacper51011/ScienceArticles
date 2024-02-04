using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Dtos.SearchPublications
{
    public class SearchPublicationsResponseItemDto
    {

        public string PublicationId { get; set; }
        public string Title { get; set; }
        public string PublicationYear { get; set; }
        public string Abstract {  get; set; }
        public string TextLink { get; set; }
        public List<string> Authors { get; set; }
    }
}
