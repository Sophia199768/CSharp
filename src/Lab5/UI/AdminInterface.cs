using Spectre.Console;

namespace UI;

public class AdminInterface
{
    public CommandsInterface? CommandsInterface { get; private set; }

    public void SetCommand(CommandsInterface commandsInterface)
    {
        CommandsInterface = commandsInterface;
    }

    public void EntranceAdmin()
    {
        string password = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter [green]password[/]?")
                .PromptStyle("red")
                .Secret());
        if (password == Config.AdminPassword)
        {
            SelectCommandAdmin();
        }

        AnsiConsole.WriteLine($"Сheck you password something went wrong");
        AnsiConsole.Ask<string>("Сlick on any button to continue");
        CommandsInterface.Exit();
    }

    public void SelectCommandAdmin()
    {
        string command = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a command?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more commands)[/]")
                .AddChoices(new[]
                {
                    "Show balance", "Show history", "Set money", "Get money", "Exit",
                }));

        if (command == "Exit") CommandsInterface.Exit();

        int id = AnsiConsole.Prompt(
            new TextPrompt<int>("Enter [green]user id[/]?")
                .PromptStyle("red"));
        switch (command)
        {
            case "Show balance":
                CommandsInterface?.Balance(id);
                break;
            case "Show history":
                CommandsInterface?.History(id);
                break;
            case "Set money":
                CommandsInterface?.SetMoney(id);
                break;
            case "Get money":
                CommandsInterface?.GetMoney(id);
                break;
        }

        SelectCommandAdmin();
    }
}