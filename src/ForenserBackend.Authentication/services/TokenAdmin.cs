using ForenserBackend.Exception.HttpErrors;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForenserBackend.Authentication.services
{
    public class TokenAdmin
    {
        public byte[] key { get; set; } = Encoding.ASCII.GetBytes("bf74347a-3851-41c3-a14b-3bebabcf2e1f");

        public string Generate(TokenPayload payload) {
            var handler = new JwtSecurityTokenHandler();
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.Email, payload.UserEmail));
            ci.AddClaim(new Claim(ClaimTypes.Authentication, payload.UserId));
            ci.AddClaim(new Claim(ClaimTypes.Name, payload.UserName));


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = ci,
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddDays(2)
            };

            var token = handler.CreateToken(tokenDescriptor);
            var tokenString = handler.WriteToken(token);
            return tokenString;
        }


        public bool ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            SecurityToken validatedToken;
            handler.ValidateToken(token, new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, /// Because there is no expiration in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = "Sample",
                ValidAudience = "Sample",
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out validatedToken);

            return true;
        }
        public TokenPayload DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                SecurityToken validatedToken;
                var principal = handler.ValidateToken(token, validationParameters, out validatedToken);

                var userId = principal.FindFirst(ClaimTypes.Authentication)?.Value ?? string.Empty;
                var userEmail = principal.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
                var userName = principal.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;

                return new TokenPayload
                {
                    UserId = userId,
                    UserEmail = userEmail,
                    UserName = userName,
                };
            }
            catch
            {
                // Token validation failed
                throw new UnauthorizedException("invalid token");
            }
        }
    }
}
