namespace GuardianCore.Options;

public class DatabaseOptions
{
    public const string ConfigurationSection = "DatabaseSettings";
    public string ConnectionString { get; set; } = string.Empty;
} 