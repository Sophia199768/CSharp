using Controllers.Requests;
using Labwork5.Services;

namespace Controllers.Chain;

public class BaseScenarioChain : IScenarioChain
{
    public BaseScenarioChain(AccountService service)
    {
        Service = service;
    }

    public IScenarioChain? Next { get; set; }
    public AccountService Service { get; }
    public void SetNext(IScenarioChain nextNode)
    {
        Next = nextNode;
    }

    public virtual ControllersResult Handle(Request request, Provider provider)
    {
        if (Next is not null)
        {
            return Next.Handle(request, provider);
        }

        return new ControllersResult.NoSuchCommand("No such command");
    }
}