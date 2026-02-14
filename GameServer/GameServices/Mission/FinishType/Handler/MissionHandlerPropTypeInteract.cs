using HyacineCore.Server.Data.Config;
using HyacineCore.Server.Data.Excel;
using HyacineCore.Server.Enums.Mission;
using HyacineCore.Server.Enums.Scene;
using HyacineCore.Server.GameServer.Game.Player;

namespace HyacineCore.Server.GameServer.Game.Mission.FinishType.Handler;

[MissionFinishType(MissionFinishTypeEnum.PropTypeInteract)]
public class MissionHandlerPropTypeInteract : MissionFinishTypeHandler
{
    public override async ValueTask HandleMissionFinishType(PlayerInstance player, SubMissionInfo info, object? arg)
    {
        // this type wont be used in mission
        await ValueTask.CompletedTask;
    }

    public override async ValueTask HandleQuestFinishType(PlayerInstance player, QuestDataExcel quest,
        FinishWayExcel excel, object? arg)
    {
        var propCount = 0;
        foreach (var floor in player.SceneData?.ScenePropData ?? [])
        foreach (var group in floor.Value)
        foreach (var prop in group.Value)
            if (prop.State == (PropStateEnum)excel.ParamInt2) // interacted
                propCount++;

        await player.QuestManager!.UpdateQuestProgress(quest.QuestID, propCount);
    }
}