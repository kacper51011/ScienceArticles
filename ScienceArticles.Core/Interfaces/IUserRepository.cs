using ScienceArticles.Domain.Aggregates;

namespace ScienceArticles.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByUsernameAsync(string username);
        public Task RegisterUserAsync(User user);
        public Task<User?> AuthenticateUserAsync(string username, string password);
    }
}
