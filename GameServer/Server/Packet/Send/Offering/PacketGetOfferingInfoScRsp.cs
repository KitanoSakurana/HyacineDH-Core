using HyacineCore.Server.Database.Inventory;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Offering;

public class PacketGetOfferingInfoScRsp : BasePacket
{
    public PacketGetOfferingInfoScRsp(List<OfferingTypeData> dataList) : base(CmdIds.GetOfferingInfoScRsp)
    {
        var proto = new GetOfferingInfoScRsp
        {
            OfferingInfoList = { dataList.Select(data => data.ToProto()).ToList() }
        };

        SetData(proto);
    }
}