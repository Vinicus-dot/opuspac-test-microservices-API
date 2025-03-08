using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationService.Helper
{
    public class JwtToken
    {
        private readonly string _secretKey;

        public JwtToken(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string CreateToken(string userEmail)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userEmail), 
                new Claim(JwtRegisteredClaimNames.Sub, userEmail), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
