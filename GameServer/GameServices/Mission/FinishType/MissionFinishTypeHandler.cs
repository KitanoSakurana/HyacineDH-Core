using HyacineCore.Server.Data.Config;
using HyacineCore.Server.Data.Excel;
using HyacineCore.Server.GameServer.Game.Player;

namespace HyacineCore.Server.GameServer.Game.Mission.FinishType;

public abstract class MissionFinishTypeHandler
{
    public abstract ValueTask HandleMissionFinishType(PlayerInstance player, SubMissionInfo info, object? arg);

    public abstract ValueTask HandleQuestFinishType(PlayerInstance player, QuestDataExcel quest, FinishWayExcel excel,
        object? arg);
}