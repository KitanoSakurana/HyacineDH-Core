using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Scene;

public class PacketChangePropTimelineInfoScRsp : BasePacket
{
    public PacketChangePropTimelineInfoScRsp(uint entityId) : base(CmdIds.ChangePropTimelineInfoScRsp)
    {
        var proto = new ChangePropTimelineInfoScRsp
        {
            PropEntityId = entityId
        };

        SetData(proto);
    }
}