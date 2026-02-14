using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Recommend;

public class PacketGetBigDataAllRecommendScRsp : BasePacket
{
    public PacketGetBigDataAllRecommendScRsp(BigDataRecommendType type) : base(CmdIds.GetBigDataAllRecommendScRsp)
    {
        var proto = new GetBigDataAllRecommendScRsp
        {
            BigDataRecommendType = type
        };

        SetData(proto);
    }
}