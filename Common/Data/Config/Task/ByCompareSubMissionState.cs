using HyacineCore.Server.Enums.Task;

namespace HyacineCore.Server.Data.Config.Task;

public class ByCompareSubMissionState : PredicateConfigInfo
{
    public int SubMissionID { get; set; }
    public SubMissionStateEnum SubMissionState { get; set; }
    public bool AllStoryLine { get; set; }
}