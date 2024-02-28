namespace Postgres.Migration;

public static class Migrations
{
    public static string GetUpBd =>
        """
        Create Table UserBd (userID serial primary key, login text, password text, balance int, role text not null);
        CREATE TABLE History (id int, operations text);
        """;

    public static string GetDownBd => """DROP Table UserBd; DROP Table History""";
}