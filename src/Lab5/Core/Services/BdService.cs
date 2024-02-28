using Labwork5.Entities;
using Labwork5.Repository;

namespace Labwork5.Services;

public class BdService
{
    public BdService(IBankBd bankBd)
    {
        BankBd = bankBd;
    }

    public IBankBd BankBd { get; }

    public RepositoryResult CreateAccount(string login, string password)
    {
        if (BankBd.FindAccountLogin(login) is not null)
        {
            return new RepositoryResult.ThisLoginAlreadyExists();
        }

        Account? account = BankBd.CreateAccount(login, password);
        if (account is null)
        {
            return new RepositoryResult.UnFindAccount();
        }

        return new RepositoryResult.ReturnAccount(account);
    }

    public RepositoryResult ViewBalance(int id)
    {
        if (BankBd.FindAccountId(id) is null)
        {
            return new RepositoryResult.UnFindAccount();
        }

        return new RepositoryResult.Balance(BankBd.ShowBalance(id));
    }

    public RepositoryResult GetMoney(int id, int amountOfMoney)
    {
        if (BankBd.FindAccountId(id) is null)
        {
            return new RepositoryResult.UnFindAccount();
        }

        if (BankBd.ShowBalance(id) < amountOfMoney)
        {
            return new RepositoryResult.BalanceMistake();
        }

        BankBd.WriteHistory(id, $"-{amountOfMoney}");
        return new RepositoryResult.Balance(BankBd.GetMoney(id, amountOfMoney));
    }

    public RepositoryResult SetMoney(int id, int amountOfMoney)
    {
        if (BankBd.FindAccountId(id) is null)
        {
            return new RepositoryResult.UnFindAccount();
        }

        BankBd.WriteHistory(id, $"+{amountOfMoney}");
        return new RepositoryResult.Balance(BankBd.SetMoney(id, amountOfMoney));
    }

    public RepositoryResult ViewHistory(int id)
    {
        if (BankBd.FindAccountId(id) is null)
        {
            return new RepositoryResult.UnFindAccount();
        }

        return new RepositoryResult.History(BankBd.ShowHistory(id));
    }

    public RepositoryResult FindAccountLogin(string login)
    {
        Account? result = BankBd.FindAccountLogin(login);
        if (result is null)
        {
            return new RepositoryResult.UnFindAccount();
        }

        return new RepositoryResult.ReturnAccount(result);
    }
}