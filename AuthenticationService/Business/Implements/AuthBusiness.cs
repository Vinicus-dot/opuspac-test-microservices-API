using AuthenticationService.Business.Interfaces;
using AuthenticationService.Helper;
using AuthenticationService.Model.Request;
using AuthenticationService.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using ServiceStack.Host;

namespace AuthenticationService.Business.Implements
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IAuthRepository _authRepository;
        private readonly JwtToken _jwtToken = new (Util.GetEnvironmentVariable("ENCRYPTION_CLAIMS_KEY"));
        public AuthBusiness(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<object> AuthenticateUser(LoginRequest loginRequest)
        {
            var user = await _authRepository.GetUser(loginRequest.Email) ??
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
            var user = await _authRepository.GetUser(registerRequest.Email);

            if(user != null)
                throw new HttpException(StatusCodes.Status400BadRequest, "Este email já esta cadastrado!");

            await _authRepository.InsertUser(new()
            {
                Email = registerRequest.Email,
                Name = registerRequest.Name,
                Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password)
            });

            throw new HttpException(StatusCodes.Status201Created, "Sucesso");
        }
    }
}
