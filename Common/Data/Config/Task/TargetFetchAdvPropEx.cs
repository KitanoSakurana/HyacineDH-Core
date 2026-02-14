using HyacineCore.Server.Enums.Task;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HyacineCore.Server.Data.Config.Task;

public class TargetFetchAdvPropEx : TargetEvaluator
{
    [JsonConverter(typeof(StringEnumConverter))]
    public TargetFetchAdvPropFetchTypeEnum FetchType { get; set; }

    //public DynamicString SinglePropKey;
    public FetchAdvPropData SinglePropID { get; set; } = new();

    //public DynamicString SingleUniqueName;
    //public DynamicString[] MultiPropKey;
    //public FetchAdvPropData[] MultiPropID;
    //public DynamicString[] MultiUniqueName;
    public DynamicFloat PropGroup { get; set; } = new();
    public DynamicFloat PropIDInOwnerGroup { get; set; } = new();
}