using AuthenticationService.Business.Interfaces;
using AuthenticationService.Model.Request;
using AuthenticationService.Repository.Interfaces;
using Helper;
using ServiceStack.Host;

namespace AuthenticationService.Business.Implements
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IAuthRepository _authRepository;
        public AuthBusiness(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<object> AuthenticateUser(LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Email))
                throw new HttpException(StatusCodes.Status400BadRequest, "Email deve ser preenchido!");

            if (string.IsNullOrEmpty(loginRequest.Password))
                throw new HttpException(StatusCodes.Status400BadRequest, "Senha deve ser preenchida!");

            var user = await _authRepository.GetUser(loginRequest.Email) ??
                throw new HttpException(StatusCodes.Status400BadRequest, "Usuário não encontrado.");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);

            if (!isPasswordValid)
                throw new HttpException(StatusCodes.Status400BadRequest, "Credenciais inválidas.");

            return new
            {
                success = true,
                token = Util._jwtToken.CreateToken(user.Email)
            };
        }

        public async Task<object?> RegisterUser(RegisterRequest registerRequest)
        {
            if(string.IsNullOrEmpty(registerRequest.Name))
                throw new HttpException(StatusCodes.Status400BadRequest, "Nome deve ser preenchido!");

            if (string.IsNullOrEmpty(registerRequest.Email))
                throw new HttpException(StatusCodes.Status400BadRequest, "Nome deve ser preenchido!");

            if (string.IsNullOrEmpty(registerRequest.Password))
                throw new HttpException(StatusCodes.Status400BadRequest, "Nome deve ser preenchido!");

            if (!Util.IsValidEmail(registerRequest.Email))
                throw new HttpException(StatusCodes.Status400BadRequest, "Email inválido!");
            
            var user = await _authRepository.GetUser(registerRequest.Email);

            if(user != null)
                throw new HttpException(StatusCodes.Status400BadRequest, "Este email já esta cadastrado!");

            await _authRepository.InsertUser(new()
            {
                Email = registerRequest.Email,
                Name = registerRequest.Name,
                Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password)
            });

            return default;
        }
    }
}
