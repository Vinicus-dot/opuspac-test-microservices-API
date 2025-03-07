using Authentication.Data.Interfaces;
using Authentication.Helper;
using Authentication.Model.Request;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ServiceStack.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Data.Implements
{
    public class AuthService : IAuthService
    {
        private readonly AuthenticationServiceContext _context;
        private readonly JwtToken _jwtToken = new (Util.GetEnvironmentVariable("ENCRYPTION_CLAIMS_KEY"));
        public AuthService(AuthenticationServiceContext context)
        {
            _context = context;
        }

        public async Task<object> AuthenticateUser(LoginRequest loginRequest)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginRequest.Email) ??
                throw new HttpException(StatusCodes.Status400BadRequest, "Usuário não encontrado.");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);

            if (!isPasswordValid)
                throw new HttpException(StatusCodes.Status400BadRequest, "Credenciais inválidas.");

            return new
            {
                success = true,
                token = _jwtToken.CreateToken(user.Email)
            };
        }

        public async Task<object> RegisterUser(RegisterRequest registerRequest)
        {
            var user = new Model.Entity.User
            {
                Email = registerRequest.Email,
                Name = registerRequest.Name,
                Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password)
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            throw new HttpException(StatusCodes.Status201Created, "Sucesso");
        }
    }
}
