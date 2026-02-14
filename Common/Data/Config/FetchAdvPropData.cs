using HyacineCore.Server.Data.Config.Task;

namespace HyacineCore.Server.Data.Config;

public class FetchAdvPropData
{
    public DynamicFloat GroupID { get; set; } = new();
    public DynamicFloat ID { get; set; } = new();
}