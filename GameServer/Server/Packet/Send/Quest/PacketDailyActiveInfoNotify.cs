using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Quest;

public class PacketDailyActiveInfoNotify : BasePacket
{
    public PacketDailyActiveInfoNotify(DailyActiveInfoNotify proto) : base(CmdIds.DailyActiveInfoNotify)
    {
        SetData(proto);
    }
}
