using SqlSugar;

namespace HyacineCore.Server.Database.Tutorial;

[SugarTable("battle_college_data")]
public class BattleCollegeData : BaseDatabaseDataHelper
{
    [SugarColumn(IsJson = true)] public List<int> FinishedCollegeIdList { get; set; } = [];
}