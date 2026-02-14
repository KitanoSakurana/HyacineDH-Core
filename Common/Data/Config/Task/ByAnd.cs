using Newtonsoft.Json.Linq;

namespace HyacineCore.Server.Data.Config.Task;

public class ByAnd : PredicateConfigInfo
{
    public List<PredicateConfigInfo> PredicateList { get; set; } = [];

    public new static PredicateConfigInfo LoadFromJsonObject(JObject obj)
    {
        var info = new ByAnd
        {
            Type = obj[nameof(Type)]!.ToObject<string>()!
        };

        foreach (var item in
                 obj[nameof(PredicateList)]?.Select(x => PredicateConfigInfo.LoadFromJsonObject((x as JObject)!)) ?? [])
            info.PredicateList.Add(item);

        return info;
    }
}