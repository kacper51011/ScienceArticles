using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Dtos.SearchPublications
{
    public class SearchPublicationsResponseItemDto
    {

        public string ServiceId { get; set; }
        public string Title { get; set; }
        public string PublicationYear { get; set; }
        public string Abstract {  get; set; }
        public string FullText { get; set; }
        public List<string> Authors { get; set; }
    }
}
