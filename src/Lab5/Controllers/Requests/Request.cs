namespace Controllers.Requests;

public abstract record Request
{
    private Request()
    {
    }

    public record LoginRequest(string Login, string Password) : Request;

    public record RegistrationRequest(string Login, string Password, string Role) : Request;

    public record GetMoneyRequest(int Id, int AmountOfMoney) : Request;

    public record SetMoneyRequest(int Id, int AmountOfMoney) : Request;

    public record ShowBalanceRequest(int Id) : Request;

    public record ShowHistoryRequest(int Id) : Request;
}