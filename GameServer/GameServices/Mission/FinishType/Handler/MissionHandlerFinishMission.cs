using HyacineCore.Server.Data.Config;
using HyacineCore.Server.Data.Excel;
using HyacineCore.Server.Enums.Mission;
using HyacineCore.Server.GameServer.Game.Player;

namespace HyacineCore.Server.GameServer.Game.Mission.FinishType.Handler;

[MissionFinishType(MissionFinishTypeEnum.FinishMission)]
public class MissionHandlerFinishMission : MissionFinishTypeHandler
{
    public override async ValueTask HandleMissionFinishType(PlayerInstance player, SubMissionInfo info, object? arg)
    {
        var send = true;
        foreach (var mainMissionId in info.ParamIntList ?? [])
            if (player.MissionManager!.GetMainMissionStatus(mainMissionId) != MissionPhaseEnum.Finish)
            {
                send = false;
                break;
            }

        if (send) await player.MissionManager!.FinishSubMission(info.ID);
    }

    public override async ValueTask HandleQuestFinishType(PlayerInstance player, QuestDataExcel quest,
        FinishWayExcel excel, object? arg)
    {
        var progress = 0;
        foreach (var mainMissionId in excel.ParamIntList)
            if (player.MissionManager!.GetMainMissionStatus(mainMissionId) == MissionPhaseEnum.Finish)
                progress++;

        await player.QuestManager!.UpdateQuestProgress(quest.QuestID, progress);
    }
}