using HyacineCore.Server.Database.Inventory;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Offering;

public class PacketSubmitOfferingItemScRsp : BasePacket
{
    public PacketSubmitOfferingItemScRsp(Retcode ret, OfferingTypeData? data) : base(CmdIds.SubmitOfferingItemScRsp)
    {
        var proto = new SubmitOfferingItemScRsp
        {
            Retcode = (uint)ret
        };

        if (data != null) proto.OfferingInfo = data.ToProto();

        SetData(proto);
    }
}