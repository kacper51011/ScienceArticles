using ScienceArticles.Domain.ValueObjects;

namespace ScienceArticles.Domain.Entities
{
    public class Category
    {

        public CategoryId CategoryId { get; private set; }
        public string Name { get; private set; }

        public List<Article> Articles { get; private set; }
    }
}
