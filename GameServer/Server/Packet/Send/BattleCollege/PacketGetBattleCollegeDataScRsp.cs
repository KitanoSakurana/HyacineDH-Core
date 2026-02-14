using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.BattleCollege;

public class PacketGetBattleCollegeDataScRsp : BasePacket
{
    public PacketGetBattleCollegeDataScRsp(PlayerInstance player) : base(CmdIds.GetBattleCollegeDataScRsp)
    {
        var proto = new GetBattleCollegeDataScRsp();

        foreach (var id in player.BattleCollegeData?.FinishedCollegeIdList ?? [])
            proto.FinishedCollegeIdList.Add((uint)id);

        SetData(proto);
    }
}