using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Recommend;

public class PacketRelicSmartWearUpdatePlanScRsp : BasePacket
{
    public PacketRelicSmartWearUpdatePlanScRsp(RelicSmartWearPlan relicPlan)
        : base(CmdIds.RelicSmartWearUpdatePlanScRsp)
    {
        var proto = new RelicSmartWearUpdatePlanScRsp
        {
            RelicPlan = relicPlan
        };

        SetData(proto);
    }
}