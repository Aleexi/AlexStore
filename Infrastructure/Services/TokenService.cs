using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    public class TokenService : InterfaceTokenService
    {
        private readonly IConfiguration configuartion;
        private readonly SymmetricSecurityKey key;
        public TokenService(IConfiguration configuartion)
        {
            this.configuartion = configuartion;
            this.key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuartion["Token:Key"]));
        }

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.DisplayName)
            };

            var credentials = new SigningCredentials(this.key, SecurityAlgorithms.HmacSha384Signature);

            var tokenInformation = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = credentials,
                Issuer = configuartion["Token:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenInformation);

            return tokenHandler.WriteToken(token);
        }
    }
}