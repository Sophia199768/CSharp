using Controllers.Requests;
using Labwork5;
using Labwork5.Services;

namespace Controllers.Chain;

public class RegistrationChain : BaseScenarioChain
{
    public RegistrationChain(AccountService service)
        : base(service)
    {
    }

    public override ControllersResult Handle(Request request, Provider provider)
    {
        if (provider.User is not null)
        {
            return base.Handle(request, provider);
        }

        if (request is not Request.RegistrationRequest obj)
        {
            return new ControllersResult.NoSuchCommand("Can't done command without registration");
        }

        if (Service.DataBaseService.CreateAccount(obj.Login, obj.Password) is RepositoryResult.SuccessAccount)
        {
            return new ControllersResult.SuccessRegistration();
        }

        return new ControllersResult.MistakeOfSign("This account is already exist");
    }
}