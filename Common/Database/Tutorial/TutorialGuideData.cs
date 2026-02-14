using HyacineCore.Server.Proto;
using SqlSugar;

namespace HyacineCore.Server.Database.Tutorial;

[SugarTable("TutorialGuide")]
public class TutorialGuideData : BaseDatabaseDataHelper
{
    [SugarColumn(IsJson = true)] public Dictionary<int, TutorialStatus> Tutorials { get; set; } = [];
}