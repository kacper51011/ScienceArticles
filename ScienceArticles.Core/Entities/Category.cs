using ScienceArticles.Domain.ValueObjects;

namespace ScienceArticles.Domain.Entities
{
    public class Category
    {
        private Category(string name)
        {
            CategoryId = new CategoryId(Guid.NewGuid());
            Name = name;

        }
        public CategoryId CategoryId { get; private set; }
        public string Name { get; private set; }
        public List<Article> Articles { get; private set; } = new List<Article>();

        public static Category Create(string name)
        {
            return new Category(name);
        }

    }
}
