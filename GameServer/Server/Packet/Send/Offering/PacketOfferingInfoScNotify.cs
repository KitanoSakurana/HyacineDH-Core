using HyacineCore.Server.Database.Inventory;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Offering;

public class PacketOfferingInfoScNotify : BasePacket
{
    public PacketOfferingInfoScNotify(OfferingTypeData data) : base(CmdIds.OfferingInfoScNotify)
    {
        var proto = new OfferingInfoScNotify
        {
            OfferingInfo = data.ToProto()
        };

        SetData(proto);
    }
}