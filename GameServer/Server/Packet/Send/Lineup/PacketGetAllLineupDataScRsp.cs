using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Lineup;

public class PacketGetAllLineupDataScRsp : BasePacket
{
    public PacketGetAllLineupDataScRsp(PlayerInstance player) : base(CmdIds.GetAllLineupDataScRsp)
    {
        var proto = new GetAllLineupDataScRsp
        {
            CurIndex = (uint)player.LineupManager!.LineupData.CurLineup
        };
        foreach (var lineup in player.LineupManager.GetAllLineup()) proto.LineupList.Add(lineup.ToProto());

        SetData(proto);
    }
}