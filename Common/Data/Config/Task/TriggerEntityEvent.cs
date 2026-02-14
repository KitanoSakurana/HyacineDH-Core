namespace HyacineCore.Server.Data.Config.Task;

public class TriggerEntityEvent : TaskConfigInfo
{
    public DynamicFloat InstanceID { get; set; } = new();
}