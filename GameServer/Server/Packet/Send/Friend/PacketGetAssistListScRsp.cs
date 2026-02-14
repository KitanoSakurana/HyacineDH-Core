using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Friend;

public class PacketGetAssistListScRsp : BasePacket
{
    public PacketGetAssistListScRsp() : base(CmdIds.GetAssistListScRsp)
    {
        var proto = new GetAssistListScRsp();

        SetData(proto);
    }
}