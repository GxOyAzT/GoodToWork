using GoodToWork.AuthService.Application.Interfaces.Token;
using GoodToWork.AuthService.Infrastructure.Configurations;
using GoodToWork.AuthService.Infrastructure.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoodToWork.AuthService.Infrastructure.Token;

internal class TokenDeserializer : ITokenDeserializer
{
    private readonly JwtTokenConfig _jwtTokenConfig;

    public TokenDeserializer(
        JwtTokenConfig jwtTokenConfig)
{
        _jwtTokenConfig = jwtTokenConfig;
    }

    public string GetUserIdFromToken(string token)
    {
        var key = Encoding.ASCII.GetBytes(_jwtTokenConfig.SecretKey);
        var handler = new JwtSecurityTokenHandler();
        var validations = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        try
        {
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);

            return claims.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        catch(Exception ex)
        {
            throw new InvalidTokenException("Token is invalid");
        }
    }

    public string GetSessionIdFromToken(string token)
    {
        var key = Encoding.ASCII.GetBytes(_jwtTokenConfig.SecretKey);
        var handler = new JwtSecurityTokenHandler();
        var validations = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        try
        {
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);

            return claims.FindFirst("SessionId").Value;
        }
        catch (Exception ex)
        {
            throw new InvalidTokenException("Token is invalid");
        }
    }
}
