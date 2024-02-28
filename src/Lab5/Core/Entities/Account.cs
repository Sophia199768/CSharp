namespace Labwork5.Entities;

public class Account
{
    public Account(string password, string login, string role, int id, int amountOfMoney)
    {
        Password = password;
        Login = login;
        Role = role;
        Id = id;
        AmountOfMoney = amountOfMoney;
    }

    public string Password { get; }
    public string Login { get; }
    public string Role { get; }
    public int Id { get; }
    public int AmountOfMoney { get; }
}