using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Recommend;

public class PacketRelicSmartWearGetPinRelicScRsp : BasePacket
{
    public PacketRelicSmartWearGetPinRelicScRsp(uint avatarId) : base(CmdIds.RelicSmartWearGetPinRelicScRsp)
    {
        var proto = new RelicSmartWearGetPinRelicScRsp
        {
            AvatarId = avatarId
        };

        SetData(proto);
    }
}