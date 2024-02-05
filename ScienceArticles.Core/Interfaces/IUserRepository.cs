using ScienceArticles.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task RegisterUserAsync(User user);
        Task<User?> AuthenticateUserAsync(string username, string password);
    }
}
