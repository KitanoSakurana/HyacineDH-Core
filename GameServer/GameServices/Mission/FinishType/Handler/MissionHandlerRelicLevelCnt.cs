using HyacineCore.Server.Data.Config;
using HyacineCore.Server.Data.Excel;
using HyacineCore.Server.Enums.Mission;
using HyacineCore.Server.GameServer.Game.Player;

namespace HyacineCore.Server.GameServer.Game.Mission.FinishType.Handler;

[MissionFinishType(MissionFinishTypeEnum.RelicLevelCnt)]
public class MissionHandlerRelicLevelCnt : MissionFinishTypeHandler
{
    public override async ValueTask HandleMissionFinishType(PlayerInstance player, SubMissionInfo info, object? arg)
    {
        // this type wont be used in mission
        await ValueTask.CompletedTask;
    }

    public override async ValueTask HandleQuestFinishType(PlayerInstance player, QuestDataExcel quest,
        FinishWayExcel excel, object? arg)
    {
        var relicCount = 0;
        foreach (var relic in player.InventoryManager?.Data.RelicItems ?? [])
            if (relic.Level >= excel.ParamInt1)
                relicCount++;

        await player.QuestManager!.UpdateQuestProgress(quest.QuestID, relicCount);
    }
}