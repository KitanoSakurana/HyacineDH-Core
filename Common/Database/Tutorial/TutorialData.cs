using HyacineCore.Server.Proto;
using SqlSugar;

namespace HyacineCore.Server.Database.Tutorial;

[SugarTable("Tutorial")]
public class TutorialData : BaseDatabaseDataHelper
{
    [SugarColumn(IsJson = true)] public Dictionary<int, TutorialStatus> Tutorials { get; set; } = [];
}