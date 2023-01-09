using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OneHandTraining;

namespace AdventureWorks
{
    public class JwtManager
    {
        private readonly string? _secretKey;
        private readonly JwtOptions _jwtOptions;

        public JwtManager(IOptions<JwtOptions> options)
        {
            _secretKey = options.Value.Key;
        }

        public string GenerateJwt(string userId, string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sid,userId),
                new Claim(ClaimTypes.Email,email),
            };

            if (_secretKey != null)
            {
                var token = new JwtSecurityToken(
                    issuer: "MyApp",
                    audience: "MyUsers",
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_secretKey)),
                        SecurityAlgorithms.HmacSha256));
            
                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return "";
        }

        public ClaimsPrincipal? VerifyJwt(string token)
        {
            if (_secretKey != null)
            {
                var validationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_secretKey)),
                    ValidAudience = "MyUsers",
                    ValidIssuer = "MyApp"
                };

                try
                {
                    var principal = new JwtSecurityTokenHandler().ValidateToken(
                        token, validationParameters, out var validatedToken);

                    return principal;
                }
                catch (SecurityTokenValidationException)
                {
                    return null;
                }
            }

            return null;
        }
    }
}
