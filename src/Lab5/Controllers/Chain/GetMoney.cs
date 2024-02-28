using Controllers.Requests;
using Labwork5.Services;

namespace Controllers.Chain;

public class GetMoney : BaseScenarioChain
{
    public GetMoney(AccountService service)
        : base(service)
    {
    }

    public override ControllersResult Handle(Request request, Provider provider)
    {
        if (request is not Request.GetMoneyRequest obj)
        {
            return base.Handle(request, provider);
        }

        Service.DataBaseService.GetMoney(obj.Id, obj.AmountOfMoney);
        return new ControllersResult.Success();
    }
}