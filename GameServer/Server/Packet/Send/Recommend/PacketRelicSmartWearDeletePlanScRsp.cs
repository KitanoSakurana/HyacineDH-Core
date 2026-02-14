using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Recommend;

public class PacketRelicSmartWearDeletePlanScRsp : BasePacket
{
    public PacketRelicSmartWearDeletePlanScRsp(uint uniqueId)
        : base(CmdIds.RelicSmartWearDeletePlanScRsp)
    {
        var proto = new RelicSmartWearDeletePlanScRsp
        {
            UniqueId = uniqueId
        };

        SetData(proto);
    }
}