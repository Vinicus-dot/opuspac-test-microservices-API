using Authentication.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Data.Interfaces
{
    public interface IAuthService
    {
        public Task<object> RegisterUser(RegisterRequest registerRequest);
        public Task<object> AuthenticateUser(LoginRequest loginRequest);
    }
}
