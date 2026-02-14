using HyacineCore.Server.Data.Config;
using HyacineCore.Server.Data.Excel;
using HyacineCore.Server.Enums.Mission;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Game.Mission.FinishType.Handler;

[MissionFinishType(MissionFinishTypeEnum.RaidFinishCnt)]
public class MissionHandlerRaidFinishCnt : MissionFinishTypeHandler
{
    public override async ValueTask HandleMissionFinishType(PlayerInstance player, SubMissionInfo info, object? arg)
    {
        var finishCount =
            (info.ParamIntList ?? []).Count(raidId => player.RaidManager!.GetRaidStatus(raidId) == RaidStatus.Finish);

        if (finishCount >= info.Progress) await player.MissionManager!.FinishSubMission(info.ID);
    }

    public override async ValueTask HandleQuestFinishType(PlayerInstance player, QuestDataExcel quest,
        FinishWayExcel excel, object? arg)
    {
        // this type wont be used in quest
        var finishCount = excel.ParamIntList.Count(raidLevel =>
            player.RaidManager!.GetRaidStatus(excel.ParamInt1, raidLevel) == RaidStatus.Finish);

        await player.QuestManager!.UpdateQuestProgress(quest.QuestID, finishCount);
    }
}