using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OtoGaleri.Application.DTOs.UserDtos;
using OtoGaleri.Application.Interfaces;
using OtoGaleri.Core.Entities;
using OtoGaleri.Core.Options;

namespace OtoGaleri.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly TokenOptions _tokenOptions;

    public TokenService(IOptions<TokenOptions> tokenOptions)
    {
        _tokenOptions = tokenOptions.Value;
    }

    public TokenResponseDto CreateToken(User user)
    {
        var expiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpirationMinutes);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        var handler = new JwtSecurityTokenHandler();
        var tokenString = handler.WriteToken(tokenDescriptor);

        return new TokenResponseDto
        {
            AccessToken = tokenString,
            Expiration = expiration
        };
    }
}