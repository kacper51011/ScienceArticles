using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Dtos.SearchPublications
{
    public class SearchPublicationsRequestDto
    {
        public string QueryString { get; set; }
        public int PageSize {  get; set; }
        public bool SynonymsIncluded { get; set; }
    }
}
