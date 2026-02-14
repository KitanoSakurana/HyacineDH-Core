using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Friend;

public class PacketGetAssistHistoryScRsp : BasePacket
{
    public PacketGetAssistHistoryScRsp(PlayerInstance player) : base(CmdIds.GetAssistHistoryScRsp)
    {
        var proto = new GetAssistHistoryScRsp();

        SetData(proto);
    }
}