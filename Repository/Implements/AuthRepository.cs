using Microsoft.EntityFrameworkCore;
using Model.Authentication.Entity;
using Repository.Interfaces;

namespace Repository.Implements
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MicroServiceContext _context;
        public AuthRepository(MicroServiceContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUser(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task InsertUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
