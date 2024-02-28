using Labwork5.Entities;
using Labwork5.Repository;
using Npgsql;
using Postgres.Migration;

namespace Postgres;

public class BankDateBase : IBankBd
{
    private const int ZeroBalance = 0;
    public BankDateBase()
    {
        var configurations = new Configurations();
        string connectionString = configurations.Host +
                                  configurations.Port +
                                  configurations.UserName +
                                  configurations.Password +
                                  configurations.Database;
        DataSource = NpgsqlDataSource.Create(connectionString);
    }

    public NpgsqlDataSource DataSource { get; }

    public void SetupDataBase()
    {
        NpgsqlCommand command = DataSource.CreateCommand(Migrations.GetUpBd);
        command.ExecuteNonQuery();
    }

    public void DropDataBase()
    {
        NpgsqlCommand command = DataSource.CreateCommand(Migrations.GetDownBd);
        command.ExecuteNonQuery();
    }

    public int GetMoney(int id, int amountOfMoney)
    {
        NpgsqlCommand updateCommand = DataSource.CreateCommand($"UPDATE UserBd SET balance = balance - {amountOfMoney} WHERE userID = {id}");
        updateCommand.ExecuteNonQuery();
        return ShowBalance(id);
    }

    public int ShowBalance(int id)
    {
        NpgsqlCommand command = DataSource.CreateCommand($"SELECT balance FROM UserBd WHERE userID = {id}");
        NpgsqlDataReader reader = command.ExecuteReader();

        reader.Read();
        int balance = reader.GetInt32(0);
        return balance;
    }

    public int SetMoney(int id, int amountOfMoney)
    {
        NpgsqlCommand updateCommand = DataSource.CreateCommand($"UPDATE UserBd SET balance = balance + {amountOfMoney} WHERE userID = {id}");

        updateCommand.ExecuteNonQuery();
        return ShowBalance(id);
    }

    public Account? CreateAccount(string login, string password)
    {
        NpgsqlCommand command = DataSource.CreateCommand($"INSERT INTO UserBd(login, password, balance, role) VALUES ('{login}', '{password}',  {ZeroBalance}, 'User')");
        command.ExecuteNonQuery();
        return FindAccountLogin(login);
    }

    public void WriteHistory(int id, string operation)
    {
        NpgsqlCommand command = DataSource.CreateCommand($"INSERT INTO History(id, operations) VALUES ({id}, '{operation}')");
        command.ExecuteNonQuery();
    }

    public IEnumerable<string> ShowHistory(int id)
    {
        NpgsqlCommand command = DataSource.CreateCommand($"SELECT operations FROM History WHERE id = {id}");
        NpgsqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            yield return reader.GetString(0);
        }
    }

    public Account? FindAccountLogin(string login)
    {
        NpgsqlCommand command = DataSource.CreateCommand($"SELECT * FROM UserBd WHERE login = '{login}'");
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.Read() is false)
        {
            return null;
        }

        return new Account(reader.GetString(2), reader.GetString(1), reader.GetString(4), reader.GetInt32(0), reader.GetInt32(3));
    }

    public Account? FindAccountId(int id)
    {
        NpgsqlCommand command = DataSource.CreateCommand($"SELECT * FROM UserBd WHERE userID = {id}");
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.Read() is false)
        {
            return null;
        }

        return new Account(reader.GetString(2), reader.GetString(1), reader.GetString(4), reader.GetInt32(0), reader.GetInt32(3));
    }
}