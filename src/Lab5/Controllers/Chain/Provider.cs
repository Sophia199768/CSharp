using Controllers.Requests;
using Labwork5.Entities;

namespace Controllers.Chain;

public class Provider
{
    public Provider(IScenarioChain scenarioChain)
    {
        ScenarioChain = scenarioChain;
    }

    public Account? User { get; set; }
    public IScenarioChain ScenarioChain { get; }

    public ControllersResult Start(Request request)
    {
        return ScenarioChain.Handle(request, this);
    }
}