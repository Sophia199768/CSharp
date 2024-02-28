using Labwork5.Entities;

namespace Labwork5.Services;

public class AccountService
{
    public AccountService(BdService dataBaseService)
    {
        DataBaseService = dataBaseService;
    }

    public BdService DataBaseService { get; }
    public RepositoryResult CheckPassword(string login, string password)
    {
        if (DataBaseService.FindAccountLogin(login) is RepositoryResult.ReturnAccount account)
        {
            if (account.UserAccount.Password != password)
            {
                return new RepositoryResult.ReturnAccount(account.UserAccount);
            }

            return new RepositoryResult.UnCorrectPassword();
        }

        return new RepositoryResult.UnFindAccount();
    }

    public RepositoryResult GetMoney(Account account, int id, int amountOfMoney)
    {
        if (account.Id != id && account.Role != "admin")
        {
            return new RepositoryResult.NoAccess();
        }

        return DataBaseService.GetMoney(id, amountOfMoney);
    }
}