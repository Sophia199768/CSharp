namespace Postgres.Migration;

public class Configurations
{
    public Configurations()
    {
        Host = "Host=localhost;";
        Port = "Port=5432;";
        UserName = "Username=sophia;";
        Password = "Password=sonetka2004;";
        Database = "Database=bankbd";
    }

    public string Host { get; }
    public string Port { get; }
    public string UserName { get; }
    public string Password { get; }
    public string Database { get; }
}