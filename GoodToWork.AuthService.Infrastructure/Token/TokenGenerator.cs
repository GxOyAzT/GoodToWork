using GoodToWork.AuthService.Application.Interfaces.Token;
using GoodToWork.AuthService.Domain.Entities;
using GoodToWork.AuthService.Infrastructure.Configurations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoodToWork.AuthService.Infrastructure.Token;

internal class TokenGenerator : ITokenGenerator
{
    private readonly JwtTokenConfig _jwtTokenConfig;

    public TokenGenerator(
		JwtTokenConfig jwtTokenConfig)
    {
        _jwtTokenConfig = jwtTokenConfig;
    }

    public string GenerateToken(UserModel userModel, SessionModel sessionModel)
    {
		var tokenHandler = new JwtSecurityTokenHandler();

		var tokenKey = Encoding.UTF8.GetBytes(_jwtTokenConfig.SecretKey!);

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new Claim[]
			{
				new Claim(ClaimTypes.Name, userModel.UserName),
				new Claim(ClaimTypes.NameIdentifier, userModel.Id.ToString()),
				new Claim("SessionId", sessionModel.Id.ToString())
            }),
			Expires = DateTime.UtcNow.AddMinutes(10),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);

		return tokenHandler.WriteToken(token);
	}
}
