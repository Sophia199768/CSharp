using Controllers.Requests;
using Labwork5;
using Labwork5.Services;

namespace Controllers.Chain;

public class ShowHistory : BaseScenarioChain
{
    public ShowHistory(AccountService service)
        : base(service)
    {
    }

    public override ControllersResult Handle(Request request, Provider provider)
    {
        if (request is not Request.ShowHistoryRequest obj)
        {
            return base.Handle(request, provider);
        }

        RepositoryResult amount = Service.DataBaseService.ViewHistory(obj.Id);
        if (amount is RepositoryResult.Balance money)
        {
            return new ControllersResult.SuccessBalance(money.AmountOfMoney);
        }

        return new ControllersResult.NoSuchCommand("This account doesn't exist");
    }
}