using HyacineCore.Server.Data.Config;
using HyacineCore.Server.Data.Excel;
using HyacineCore.Server.Enums.Mission;
using HyacineCore.Server.GameServer.Game.Battle;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Game.Mission.FinishType.Handler;

[MissionFinishType(MissionFinishTypeEnum.BattleWinWithCustomValue)]
public class MissionHandlerBattleWinWithCustomValue : MissionFinishTypeHandler
{
    public override async ValueTask HandleMissionFinishType(PlayerInstance player, SubMissionInfo info, object? arg)
    {
        if (arg is not BattleInstance instance) return;
        if (!instance.StageId.ToString().StartsWith(info.ParamInt2.ToString())) return; // check stage id
        if (instance.BattleEndStatus != BattleEndStatus.BattleEndWin) return; // check battle status
        if (instance.BattleResult == null) return; // check battle result
        if (!instance.BattleResult.Stt.CustomValues.TryGetValue(info.ParamStr1, out var dValue))
            return; // check custom value is exist
        if ((int)dValue == info.ParamInt1) await player.MissionManager!.FinishSubMission(info.ID);
    }

    public override async ValueTask HandleQuestFinishType(PlayerInstance player, QuestDataExcel quest,
        FinishWayExcel excel, object? arg)
    {
        // this type wont be used in quest
        await ValueTask.CompletedTask;
    }
}