using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace explore_.net.Helpers
{
    public class JwtService
    {
        private string secureKey;
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            secureKey = _configuration["Jwt:Key"];
        }

        public string GenerateToken(int id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);
            // Valid on year!!!
            var payload = new JwtPayload
            {
                { "id", id },
                { "iss", _configuration["Jwt:Issuer"] },
                { "exp", DateTimeOffset.UtcNow.AddDays(1).ToUnixTimeSeconds() } // Token expiration time
            };
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Vertify(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);
            tokenHandler.ValidateToken(token,
                new TokenValidationParameters {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateIssuer = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateAudience = true
                }, out SecurityToken securityToken);

            return (JwtSecurityToken)securityToken;
        }
    }
}

