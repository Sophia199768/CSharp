using Labwork5.Entities;

namespace Labwork5.Repository;

public interface IBankBd
{
    public int GetMoney(int id, int amountOfMoney);
    public int ShowBalance(int id);
    public int SetMoney(int id, int amountOfMoney);
    public Account? CreateAccount(string login, string password);
    public void WriteHistory(int id, string operation);
    public IEnumerable<string> ShowHistory(int id);
    public Account? FindAccountLogin(string login);
    public Account? FindAccountId(int id);
}