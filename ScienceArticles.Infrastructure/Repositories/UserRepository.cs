using Microsoft.EntityFrameworkCore;
using ScienceArticles.Domain.Aggregates;
using ScienceArticles.Domain.Interfaces;
using ScienceArticles.Infrastructure.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<User?> AuthenticateUserAsync(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task RegisterUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
    }
}
