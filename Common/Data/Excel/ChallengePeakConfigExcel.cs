using HyacineCore.Server.Util;
using Newtonsoft.Json;

namespace HyacineCore.Server.Data.Excel;

[ResourceEntity("ChallengePeakConfig.json")]
public class ChallengePeakConfigExcel : ExcelResource
{
    public int ID { get; set; }
    public int MapEntranceID { get; set; }
    public int MazeGroupID { get; set; }
    public List<int> TagList { get; set; } = [];
    public List<int> HPProgressValueList { get; set; } = [];
    public List<int> ProgressValueList { get; set; } = [];
    public List<int> EventIDList { get; set; } = [];
    public List<int> NpcMonsterIDList { get; set; } = [];
    public List<int> NormalTargetList { get; set; } = [];

    [JsonIgnore]
    public Dictionary<int, List<ChallengeConfigExcel.ChallengeMonsterInfo>> ChallengeMonsters { get; } = [];

    [JsonIgnore] public ChallengePeakBossConfigExcel? BossExcel { get; set; }

    public override int GetId()
    {
        return ID;
    }

    public override void Loaded()
    {
        GameData.ChallengePeakConfigData.TryAdd(ID, this);
    }

    public override void AfterAllDone()
    {
        RebuildChallengeMonsters();
    }

    public void RebuildChallengeMonsters()
    {
        var groupId = MazeGroupID;
        if (groupId <= 0)
        {
            // Backward compatibility for older configs that miss MazeGroupID.
            var peakGroupId = GameConstants.ResolveChallengePeakGroupIdByLevel(ID);
            groupId = GameConstants.ResolveChallengePeakStartGroupId(peakGroupId, false);
        }

        if (groupId <= 0) return;

        ChallengeMonsters[groupId] = [];

        var curConfId = 200000;
        var count = Math.Max(EventIDList.Count, NpcMonsterIDList.Count);
        for (var i = 0; i < count; i++)
        {
            var eventId = i < EventIDList.Count ? EventIDList[i] : 0;
            if (eventId <= 0) continue;

            var npcMonsterId = i < NpcMonsterIDList.Count ? NpcMonsterIDList[i] : 0;

            // Fallback for resource variants that omit NpcMonsterIDList.
            if (npcMonsterId <= 0)
            {
            // get from stage id
            if (!GameData.StageConfigData.TryGetValue(eventId, out var stage)) continue;

            var monsterId = stage.MonsterList.LastOrDefault()?.Monster0 ?? 0;
            if (!GameData.MonsterConfigData.TryGetValue(monsterId, out var monsterConf)) continue;
            if (!GameData.MonsterTemplateConfigData.TryGetValue(monsterConf.MonsterTemplateID, out var template)) continue;

                npcMonsterId = template.NPCMonsterList.Take(2).LastOrDefault(0);
            }

            if (npcMonsterId <= 0 || !GameData.NpcMonsterDataData.ContainsKey(npcMonsterId)) continue;

            ChallengeMonsters[groupId].Add(
                new ChallengeConfigExcel.ChallengeMonsterInfo(++curConfId, npcMonsterId, eventId));
        }
    }
}
