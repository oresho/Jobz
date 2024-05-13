using JobzApi.Models.Entities;
using JobzApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobzApi.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        public JwtService(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _configuration = configuration;

        }
        public async Task<string> GenerateTokenAsync(AppUser appUser)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]);
            var expirationDate = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationTime"]));
            var claims = await GetClaimsAsync(appUser);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expirationDate,
                IssuedAt = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            throw new NotImplementedException();
        }

        private async Task<Claim[]> GetClaimsAsync(AppUser appUser)
        {
            List<Claim> claims = new List<Claim>();
            var roles = await _userManager.GetRolesAsync(appUser);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            claims.Add(new Claim("Fullname", $"{appUser.FirstName} {appUser.LastName}"));
            claims.Add(new Claim("Email", $"{appUser.Email}"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            return claims.ToArray();
        }
    }
}
