using HyacineCore.Server.Data.Config;
using HyacineCore.Server.Data.Excel;
using HyacineCore.Server.Enums.Mission;
using HyacineCore.Server.GameServer.Game.Player;

namespace HyacineCore.Server.GameServer.Game.Mission.FinishType.Handler;

[MissionFinishType(MissionFinishTypeEnum.AnyEquipmentPromoteCnt)]
public class MissionHandlerAnyEquipmentPromoteCnt : MissionFinishTypeHandler
{
    public override async ValueTask HandleMissionFinishType(PlayerInstance player, SubMissionInfo info, object? arg)
    {
        // this type wont be used in mission
        await ValueTask.CompletedTask;
    }

    public override async ValueTask HandleQuestFinishType(PlayerInstance player, QuestDataExcel quest,
        FinishWayExcel excel, object? arg)
    {
        var promoteCount = 0;
        foreach (var equipment in player.InventoryManager?.Data.EquipmentItems ?? [])
            promoteCount += equipment.Promotion - 1;
        await player.QuestManager!.UpdateQuestProgress(quest.QuestID, promoteCount);
    }
}