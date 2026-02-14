using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Activity;

public class PacketGetTrialActivityDataScRsp : BasePacket
{
    public PacketGetTrialActivityDataScRsp(PlayerInstance player) : base(CmdIds.GetTrialActivityDataScRsp)
    {
        var proto = new GetTrialActivityDataScRsp();
        proto.TrialActivityInfoList.Add(player.ActivityManager!.Data.TrialActivityData.ToProto());
        SetData(proto);
    }
}