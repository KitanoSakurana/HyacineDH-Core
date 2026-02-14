using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Recommend;

public class PacketRelicSmartWearAddPlanScRsp : BasePacket
{
    public PacketRelicSmartWearAddPlanScRsp(RelicSmartWearPlan addPlan) : base(CmdIds.RelicSmartWearAddPlanScRsp)
    {
        var proto = new RelicSmartWearAddPlanScRsp
        {
            RelicPlan = addPlan
        };

        SetData(proto);
    }
}