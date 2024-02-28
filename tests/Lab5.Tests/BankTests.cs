using Labwork5;
using Labwork5.Entities;
using Labwork5.Repository;
using Labwork5.Services;
using NSubstitute;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests;

public class BankTests
{
    [Fact]
    public void Withdrawing_Money_From_Account_With_Sufficient_Balance()
    {
        // Arrange
        IBankBd repositoryMock = Substitute.For<IBankBd>();
        int twelveThousands = 12000;
        int id = 1;
        int currentbalance = 70778;
        repositoryMock.FindAccountId(id).Returns(new Account("12345", "soneta", "User", 1, currentbalance));
        repositoryMock.ShowBalance(id).Returns(currentbalance);
        repositoryMock.GetMoney(id, twelveThousands).Returns(currentbalance - twelveThousands);
        var bdService = new BdService(repositoryMock);

        // Act
        RepositoryResult result = bdService.GetMoney(id, twelveThousands);

        // Assert
        Assert.True(result is RepositoryResult.Balance);
        if (result is RepositoryResult.Balance balance)
        {
            Assert.Equal(balance.AmountOfMoney, currentbalance - twelveThousands);
        }
    }

    [Fact]
    public void Withdrawal_Of_Money_In_Case_Of_Insufficient_Balance()
    {
        // Arrange
        IBankBd repositoryMock = Substitute.For<IBankBd>();
        int million = 1000000;
        int id = 1;
        int currentbalance = 70778;
        repositoryMock.FindAccountId(id).Returns(new Account("12345", "soneta", "User", 1, currentbalance));
        repositoryMock.ShowBalance(id).Returns(currentbalance);
        repositoryMock.GetMoney(id, million).Returns(currentbalance);
        var bdService = new BdService(repositoryMock);

        // Act
        RepositoryResult result = bdService.GetMoney(id, million);

        // Assert
        Assert.True(result is RepositoryResult.BalanceMistake);
    }

    [Fact]
    public void Saving_Account_With_Correctly_Updated_Balance()
    {
        // Arrange
        IBankBd repositoryMock = Substitute.For<IBankBd>();
        int tvelveThousands = 12000;
        int elevenThousand = 11000;
        int id = 1;
        int currentbalance = 70778;
        repositoryMock.FindAccountId(id).Returns(new Account("12345", "soneta", "User", 1, currentbalance));
        repositoryMock.ShowBalance(id).Returns(currentbalance);
        repositoryMock.GetMoney(id, tvelveThousands).Returns(currentbalance - tvelveThousands);
        repositoryMock.ShowBalance(id).Returns(currentbalance - tvelveThousands);
        repositoryMock.SetMoney(id, elevenThousand).Returns(currentbalance - tvelveThousands + elevenThousand);
        var bdService = new BdService(repositoryMock);

        // Act
        bdService.GetMoney(id, tvelveThousands);
        RepositoryResult result = bdService.SetMoney(id, elevenThousand);

        // Assert
        Assert.True(result is RepositoryResult.Balance);
        if (result is RepositoryResult.Balance balance)
        {
            Assert.Equal(balance.AmountOfMoney, currentbalance - tvelveThousands + elevenThousand);
        }
    }
}