using ScienceArticles.Domain.Entities;
using ScienceArticles.Domain.ValueObjects;

namespace ScienceArticles.Domain.Aggregates
{
    public class User
    {
        public UserId UserId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public List<Article> Articles { get; private set; } = new List<Article>();


        public static User Create(string username, string password)
        {
            return new User()
            {
                UserId = new UserId(Guid.NewGuid()),
                Username = username,
                Password = password,
                Articles = new List<Article>()
            };
        }

    }


}
