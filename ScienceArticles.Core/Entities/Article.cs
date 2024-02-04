using ScienceArticles.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Domain.Entities
{
    public class Article
    {
        public ArticleId ArticleId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }

        public CategoryId CategoryId { get; private set; }
        public Category Category { get; private set; }
    }
}
