using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Message;

public class PacketFinishPerformSectionIdScRsp : BasePacket
{
    public PacketFinishPerformSectionIdScRsp(uint sectionId) : base(CmdIds.FinishPerformSectionIdScRsp)
    {
        var proto = new FinishPerformSectionIdScRsp
        {
            SectionId = sectionId
        };

        SetData(proto);
    }
}