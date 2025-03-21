﻿using AuthenticationService.Business;
using AuthenticationService.Model.Entity;
using AuthenticationService.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Repository.Implements
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthenticationServiceContext _context;
        public AuthRepository(AuthenticationServiceContext context)
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
