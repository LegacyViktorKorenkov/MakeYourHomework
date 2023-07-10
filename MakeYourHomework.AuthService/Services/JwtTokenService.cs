using MakeYourHomework.AuthService.Models;
using MakeYourHomework.AuthService.Services.Abstraction;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MakeYourHomework.AuthService.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> CreateTokenAsync(User tokenSubject)
    {
        return await Task.Run(CreateToken);

        string CreateToken()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, tokenSubject.UserName),
                new Claim("Nickname", tokenSubject.Nickname),
                new Claim(ClaimTypes.Email, tokenSubject.Email),
                new Claim(ClaimTypes.Role, tokenSubject.UserType.ToString())
            };
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Secret").Value));
            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(5),
                signingCredentials: credentials);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenString = jwtSecurityTokenHandler.WriteToken(tokenOptions);

            return tokenString;
        }
    }
}
