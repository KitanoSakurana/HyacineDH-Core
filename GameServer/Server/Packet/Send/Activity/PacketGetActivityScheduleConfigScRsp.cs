using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Activity;

public class PacketGetActivityScheduleConfigScRsp : BasePacket
{
    public PacketGetActivityScheduleConfigScRsp(PlayerInstance player) : base(CmdIds.GetActivityScheduleConfigScRsp)
    {
        var proto = new GetActivityScheduleConfigScRsp();

        proto.ScheduleData.AddRange(player.ActivityManager!.ToProto());

        SetData(proto);
    }
}