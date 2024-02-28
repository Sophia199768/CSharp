using Labwork5;
using Labwork5.Services;
using Spectre.Console;

namespace UI;

public class CommandsInterface
{
    public CommandsInterface(AccountService accountService, AdminInterface adminInterface, UserInterface userInterface)
    {
        AccountService = accountService;
        AdminInterface = adminInterface;
        UserInterface = userInterface;
    }

    public AccountService AccountService { get; }
    public AdminInterface AdminInterface { get; }
    public UserInterface UserInterface { get; }

    public static void Exit()
    {
        AnsiConsole.WriteLine($"Exit from programm!");
        System.Environment.Exit(0);
    }

    public void SelectRole()
    {
        string role = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a role?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more roles)[/]")
                .AddChoices(new[]
                {
                    "User", "Admin", "Exit",
                }));
        AnsiConsole.WriteLine($"You choose a role : {role} Well done!");
        switch (role)
        {
            case "Admin":
                AdminInterface.EntranceAdmin();
                break;
            case "User":
                UserInterface.EntranceUser();
                break;
            case "Exit":
                Exit();
                break;
        }
    }

    public void History(int id)
    {
        AnsiConsole.Write(new Markup("[purple]You start watching history[/] "));
        AnsiConsole.WriteLine();
        var table = new Table();
        table.AddColumn("Changes");
        RepositoryResult history = AccountService.DataBaseService.ViewHistory(id);

        if (history is RepositoryResult.History operations)
        {
            foreach (string current in operations.Operations)
            {
                table.AddRow($"{current}");
            }

            AnsiConsole.Write(table);
        }
        else
        {
            AnsiConsole.WriteLine($"There are no such user id");
        }

        AnsiConsole.Ask<string>("Сlick on any button to continue");
    }

    public void Balance(int id)
    {
        AnsiConsole.Write(new Markup("[purple]You start watching your balance[/] "));
        RepositoryResult result = AccountService.DataBaseService.ViewBalance(id);
        AnsiConsole.Write(new Markup("[bold yellow]Your balance is[/] [red][/]"));
        if (result is RepositoryResult.Balance balance)
        {
            AnsiConsole.WriteLine($"{balance.AmountOfMoney}");
        }

        AnsiConsole.Ask<string>("Сlick on any button to continue");
    }

    public void GetMoney(int id)
    {
        AnsiConsole.Write(new Markup("[purple]You start get money[/] "));
        int amountOfMoney = AnsiConsole.Prompt(
            new TextPrompt<int>("Write how much money you want to [green]get[/]?")
                .PromptStyle("red"));

        RepositoryResult result = AccountService.DataBaseService.GetMoney(id, amountOfMoney);
        switch (result)
        {
            case RepositoryResult.Balance balance:
                AnsiConsole.WriteLine($"Your new balance is : {balance.AmountOfMoney}");
                break;
            case RepositoryResult.BalanceMistake:
                AnsiConsole.WriteLine("Balance can't be negative");
                break;
        }

        AnsiConsole.Ask<string>("Сlick on any button to continue");
    }

    public void SetMoney(int id)
    {
        AnsiConsole.Write(new Markup("[purple]You start set money[/] "));
        AnsiConsole.WriteLine();
        int amountOfMoney = AnsiConsole.Prompt(
            new TextPrompt<int>("Write how much money you want to [green]set[/]?")
                .PromptStyle("red"));

        RepositoryResult result = AccountService.DataBaseService.SetMoney(id, amountOfMoney);
        switch (result)
        {
            case RepositoryResult.Balance balance:
                AnsiConsole.WriteLine($"Your new balance is : {balance.AmountOfMoney}");
                break;
            case RepositoryResult.BalanceMistake:
                AnsiConsole.WriteLine("You haven't got enough money");
                break;
        }

        AnsiConsole.Ask<string>("Сlick on any button to continue");
    }
}