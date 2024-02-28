using Controllers.Requests;
using Labwork5;
using Labwork5.Services;

namespace Controllers.Chain;

public class SignInChain : BaseScenarioChain
{
    public SignInChain(AccountService service)
        : base(service)
    {
    }

    public override ControllersResult Handle(Request request, Provider provider)
    {
        if (provider.User is not null || request is Request.RegistrationRequest)
        {
            return base.Handle(request, provider);
        }

        if (request is not Request.LoginRequest obj)
        {
            return new ControllersResult.NoSuchCommand("Can't done command without registration");
        }

        if (Service.CheckPassword(obj.Login, obj.Password) is RepositoryResult.ReturnAccount account)
        {
            provider.User = account.UserAccount;
        }

        return new ControllersResult.MistakeOfSign("Check your login or password, something uncorrect");
    }
}