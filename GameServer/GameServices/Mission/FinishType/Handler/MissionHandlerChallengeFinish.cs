using HyacineCore.Server.Data.Config;
using HyacineCore.Server.Data.Excel;
using HyacineCore.Server.Enums.Mission;
using HyacineCore.Server.GameServer.Game.Challenge.Definitions;
using HyacineCore.Server.GameServer.Game.Player;

namespace HyacineCore.Server.GameServer.Game.Mission.FinishType.Handler;

[MissionFinishType(MissionFinishTypeEnum.ChallengeFinish)]
public class MissionHandlerChallengeFinish : MissionFinishTypeHandler
{
    public override async ValueTask HandleMissionFinishType(PlayerInstance player, SubMissionInfo info, object? arg)
    {
        if (arg is BaseLegacyChallengeInstance challenge)
            if (challenge.Config.ID == info.ParamInt1 && challenge.IsWin)
                await player.MissionManager!.FinishSubMission(info.ID);
    }

    public override async ValueTask HandleQuestFinishType(PlayerInstance player, QuestDataExcel quest,
        FinishWayExcel excel, object? arg)
    {
        if (arg is BaseLegacyChallengeInstance challenge)
            if (challenge.Config.ID == excel.ParamInt1 && challenge.IsWin)
                await player.QuestManager!.AddQuestProgress(quest.QuestID, 1);
    }
}