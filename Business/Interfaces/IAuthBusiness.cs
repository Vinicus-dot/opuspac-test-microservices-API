using Model.Authentication.Request;

namespace AuthenticationService.Business.Interfaces
{
    public interface IAuthBusiness
    {
        public Task<object?> RegisterUser(RegisterRequest registerRequest);
        public Task<object> AuthenticateUser(LoginRequest loginRequest);
    }
}
