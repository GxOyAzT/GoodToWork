namespace GoodToWork.AuthService.Infrastructure.Configurations;

public class DatabaseConfig
{
    public const string SectionName = "Database";

    public string? ConnectionString { get; set; }
}
