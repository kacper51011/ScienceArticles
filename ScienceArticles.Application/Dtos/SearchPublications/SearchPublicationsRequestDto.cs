using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Dtos.SearchPublications
{
    public class SearchPublicationsRequestDto
    {

        [Required]
        public string QueryString { get; set; }
        public int PageSize { get; set; } = 50;
        public bool SynonymsIncluded { get; set; } = true;
    }
}
