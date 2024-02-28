using Labwork5.Services;

namespace UI;

public static class ApplicationSetup
{
    public static CommandsInterface Setup()
    {
        var dataBase = new Postgres.BankDateBase();
        var bdService = new BdService(dataBase);
        var accountService = new AccountService(bdService);
        var adminInterface = new AdminInterface();
        var userInterface = new UserInterface();
        var commandInterface = new CommandsInterface(accountService, adminInterface, userInterface);
        adminInterface.SetCommand(commandInterface);
        userInterface.SetCommand(commandInterface);
        return commandInterface;
    }
}