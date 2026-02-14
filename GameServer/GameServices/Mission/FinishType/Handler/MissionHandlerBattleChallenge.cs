using HyacineCore.Server.Data.Config;
using HyacineCore.Server.Data.Excel;
using HyacineCore.Server.Enums.Mission;
using HyacineCore.Server.GameServer.Game.Battle;
using HyacineCore.Server.GameServer.Game.Player;

namespace HyacineCore.Server.GameServer.Game.Mission.FinishType.Handler;

[MissionFinishType(MissionFinishTypeEnum.BattleChallenge)]
public class MissionHandlerBattleChallenge : MissionFinishTypeHandler
{
    public override async ValueTask HandleMissionFinishType(PlayerInstance player, SubMissionInfo info, object? arg)
    {
        // this type wont be used in mission
        await ValueTask.CompletedTask;
    }

    public override async ValueTask HandleQuestFinishType(PlayerInstance player, QuestDataExcel quest,
        FinishWayExcel excel, object? arg)
    {
        if (arg is BattleInstance instance)
        {
            var progress = 0;
            if (instance.BattleResult == null) return;
            foreach (var battleTargetList in instance.BattleResult.Stt.BattleTargetInfo.Values)
            foreach (var battleTarget in battleTargetList.BattleTargetList_)
                if (excel.ParamIntList.Contains((int)battleTarget.Id))
                    progress += (int)battleTarget.Progress;

            await player.QuestManager!.UpdateQuestProgress(quest.QuestID, progress);
        }
    }
}