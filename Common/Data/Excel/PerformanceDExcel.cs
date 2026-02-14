using HyacineCore.Server.Data.Config;
using Newtonsoft.Json;

namespace HyacineCore.Server.Data.Excel;

[ResourceEntity("PerformanceD.json")]
public class PerformanceDExcel : ExcelResource
{
    public int PerformanceID { get; set; }
    public string PerformancePath { get; set; } = "";

    [JsonIgnore] public LevelGraphConfigInfo? ActInfo { get; set; }

    public override int GetId()
    {
        return PerformanceID;
    }

    public override void Loaded()
    {
        GameData.PerformanceDData.Add(PerformanceID, this);
    }
}