using HyacineCore.Server.GameServer.Game.Player;

namespace HyacineCore.Server.GameServer.Game.Mission.FinishAction;

public abstract class MissionFinishActionHandler
{
    public abstract ValueTask OnHandle(List<int> @params, List<string> paramString, PlayerInstance player);
}