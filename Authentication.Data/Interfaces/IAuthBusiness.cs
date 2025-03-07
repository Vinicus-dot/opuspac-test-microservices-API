using Authentication.Model.Request;

namespace Authentication.Business.Interfaces
{
    public interface IAuthBusiness
    {
        public Task<object> RegisterUser(RegisterRequest registerRequest);
        public Task<object> AuthenticateUser(LoginRequest loginRequest);
    }
}
