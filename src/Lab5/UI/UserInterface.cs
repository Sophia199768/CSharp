using Labwork5;
using Labwork5.Entities;
using Spectre.Console;

namespace UI;

public class UserInterface
{
    public CommandsInterface? CommandsInterface { get; private set; }

    public Account? Account { get; set; }
    public void SetCommand(CommandsInterface commandsInterface)
    {
        CommandsInterface = commandsInterface;
    }

    public void EntranceUser()
    {
        string entrance = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Log in or registration in system?")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Log in", "Registration",
                }));
        if (entrance == "Log in")
        {
            string login = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter [green]login[/]?")
                    .PromptStyle("red"));
            string password = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter [green]password[/]?")
                    .PromptStyle("red")
                    .Secret());
            if (CommandsInterface?.AccountService.CheckPassword(login, password) is RepositoryResult.ReturnAccount account)
            {
                Account = account.UserAccount;
                SelectCommandUser();
            }

            AnsiConsole.WriteLine($"Сheck you login or password something went wrong");
            AnsiConsole.Ask<string>("Сlick on any button to continue");
            CommandsInterface?.SelectRole();
        }
        else
        {
            string login = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter [green]login[/]?")
                    .PromptStyle("red"));
            string password = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter [green]password[/]?")
                    .PromptStyle("red")
                    .Secret());
            RepositoryResult result = CommandsInterface?.AccountService.DataBaseService.CreateAccount(login, password) ?? new RepositoryResult.NoAccess();
            if (result is RepositoryResult.ReturnAccount account)
            {
                Account = account.UserAccount;
                AnsiConsole.WriteLine($"You was registrated successfully : {entrance}");
                AnsiConsole.Ask<string>("Сlick on any button to continue");
                SelectCommandUser();
            }

            if (result is RepositoryResult.ThisLoginAlreadyExists)
            {
                AnsiConsole.WriteLine($"This is untaken login");
                CommandsInterface?.SelectRole();
            }
        }
    }

    public void SelectCommandUser()
    {
        if (Account is null) return;
        string command = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a command?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more commands)[/]")
                .AddChoices(new[]
                {
                   "Show balance", "Show history", "Set money", "Get money", "Exit",
                }));
        switch (command)
        {
            case "Show balance":
                CommandsInterface?.Balance(Account.Id);
                break;
            case "Show history":
                CommandsInterface?.History(Account.Id);
                break;
            case "Set money":
                CommandsInterface?.SetMoney(Account.Id);
                break;
            case "Get money":
                CommandsInterface?.GetMoney(Account.Id);
                break;
            case "Exit":
                CommandsInterface.Exit();
                break;
        }

        SelectCommandUser();
    }
}