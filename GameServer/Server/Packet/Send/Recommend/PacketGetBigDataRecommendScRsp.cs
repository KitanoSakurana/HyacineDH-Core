using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Recommend;

public class PacketGetBigDataRecommendScRsp : BasePacket
{
    public PacketGetBigDataRecommendScRsp(uint avatarId, BigDataRecommendType type)
        : base(CmdIds.GetBigDataRecommendScRsp)
    {
        var proto = new GetBigDataRecommendScRsp
        {
            HasRecommand = true,
            EquipAvatar = avatarId,
            BigDataRecommendType = type
        };

        SetData(proto);
    }
}