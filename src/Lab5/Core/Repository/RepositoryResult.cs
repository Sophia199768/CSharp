using Labwork5.Entities;

namespace Labwork5;

public abstract record RepositoryResult
{
    private RepositoryResult()
    {
    }

    public record ThisLoginAlreadyExists() : RepositoryResult;
    public record UnFindAccount() : RepositoryResult;
    public record BalanceMistake() : RepositoryResult;

    public record Balance(int AmountOfMoney) : RepositoryResult;
    public record History(IEnumerable<string> Operations) : RepositoryResult;

    public record ReturnAccount(Account UserAccount) : RepositoryResult;

    public record UnCorrectPassword() : RepositoryResult;

    public record NoAccess() : RepositoryResult;
}