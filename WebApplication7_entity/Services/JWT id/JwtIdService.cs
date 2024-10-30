using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication7_petPals.Services.JWT_id
{
    public class JwtIdService : IJwtIdInterface
    {
        private readonly string SecretKey;
        public JwtIdService(IConfiguration configuration)

        {
            SecretKey = configuration["Jwt:Key"];
        }
        public int GetUserFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityKey = Encoding.UTF8.GetBytes(SecretKey);
                var validationParameter = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
                var principal = tokenHandler.ValidateToken(token, validationParameter, out var validatedToken);
                if (validatedToken is not JwtSecurityToken jwtToken)
                {
                    throw new SecurityTokenException("Inavlid jwt token");

                }
                var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var id))
                {
                    throw new SecurityTokenException(" invalid missing id");

                }
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error fetching id from token {ex.Message}");
                return 0;
            }
        }
    }
}
    
