namespace GoodToWork.AuthService.Infrastructure.Configurations;

public class JwtTokenConfig
{
    public const string SectionName = "JwtToken";

    public string? SecretKey { get; set; }
}
