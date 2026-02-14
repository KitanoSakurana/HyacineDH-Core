using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Lineup;

public class PacketExtraLineupDestroyNotify : BasePacket
{
    public PacketExtraLineupDestroyNotify(ExtraLineupType type) : base(CmdIds.ExtraLineupDestroyNotify)
    {
        var proto = new ExtraLineupDestroyNotify
        {
            ExtraLineupType = type
        };

        SetData(proto);
    }
}