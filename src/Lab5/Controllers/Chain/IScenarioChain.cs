using Controllers.Requests;

namespace Controllers.Chain;

public interface IScenarioChain
{
    public void SetNext(IScenarioChain nextNode);
    public ControllersResult Handle(Request request, Provider provider);
}