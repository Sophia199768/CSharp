using Controllers.Requests;
using Labwork5.Services;

namespace Controllers.Chain;

public class SetMoney : BaseScenarioChain
{
    public SetMoney(AccountService service)
        : base(service)
    {
    }

    public override ControllersResult Handle(Request request, Provider provider)
    {
        if (request is not Request.SetMoneyRequest obj)
        {
            return base.Handle(request, provider);
        }

        Service.DataBaseService.SetMoney(obj.Id, obj.AmountOfMoney);
        return new ControllersResult.Success();
    }
}