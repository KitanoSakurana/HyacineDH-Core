using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Lineup;

public class PacketGetCurLineupDataScRsp : BasePacket
{
    public PacketGetCurLineupDataScRsp(PlayerInstance player) : base(CmdIds.GetCurLineupDataScRsp)
    {
        var data = new GetCurLineupDataScRsp
        {
            Lineup = player.LineupManager?.GetCurLineup()?.ToProto() ?? new LineupInfo()
        };

        SetData(data);
    }
}