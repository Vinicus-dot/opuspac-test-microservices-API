using Authentication.Business.Interfaces;
using Authentication.Model.Request;
using Authentication.Model.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthenticationService.Controllers
{
    public class AuthController : GenericController
    {
        private readonly IAuthBusiness _authBusiness;
        public AuthController(IAuthBusiness authService)
        {
            _authBusiness = authService;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <remarks>
        /// This endpoint creates a new user with name, email, and password.
        /// </remarks>
        /// <param name="request">User registration data</param>
        /// <response code="201">User successfully created</response>
        /// <response code="400">Malformed request</response>
        /// <response code="409">User already exists</response>
        /// <response code="500">Internal server error</response>
        [SwaggerResponse(201, "User successfully created.")]
        [SwaggerResponse(400, "Malformed request.")]
        [SwaggerResponse(409, "User already exists.")]
        [SwaggerResponse(500, "Internal server error.")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            return Ok(await _authBusiness.RegisterUser(request));
        }

        /// <summary>
        /// Authenticate a user and return a JWT token
        /// </summary>
        /// <remarks>
        /// This endpoint verifies user credentials and returns a JWT token upon success.
        /// </remarks>
        /// <param name="request">User login credentials</param>
        /// <response code="200">Successful authentication</response>
        /// <response code="400">Malformed request</response>
        /// <response code="401">Invalid credentials</response>
        /// <response code="500">Internal server error</response>
        [SwaggerResponse(200, "Successful authentication.", typeof(AuthResponse))]
        [SwaggerResponse(400, "Malformed request.")]
        [SwaggerResponse(401, "Invalid credentials.")]
        [SwaggerResponse(500, "Internal server error.")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            return Ok(await _authBusiness.AuthenticateUser(request));
        }

    }
}
