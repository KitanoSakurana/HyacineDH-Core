using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Player;

public class PacketGetLevelRewardTakenListScRsp : BasePacket
{
    public PacketGetLevelRewardTakenListScRsp(PlayerInstance player) : base(CmdIds.GetLevelRewardTakenListScRsp)
    {
        var proto = new GetLevelRewardTakenListScRsp
        {
            LevelRewardTakenList = { player.Data.TakenLevelReward.Select(x => (uint)x) }
        };

        SetData(proto);
    }
}